using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CodeZero.Shared.Extensions.Collections
{
    /// <summary> 
    /// Extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Concatenates the members of a constructed <see cref="IEnumerable{T}"/> collection of type System.String, using the specified separator between each member.
        /// This is a shortcut for string.Join(...)
        /// </summary>
        /// <param name="source">A collection that contains the strings to concatenate.</param>
        /// <param name="separator">The string to use as a separator. separator is included in the returned string only if values has more than one element.</param>
        /// <returns>A string that consists of the members of values delimited by the separator string. If values has no members, the method returns System.String.Empty.</returns>
        public static string JoinAsString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// This is a shortcut for string.Join(...)
        /// </summary>
        /// <param name="source">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. separator is included in the returned string only if values has more than one element.</param>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <returns>A string that consists of the members of values delimited by the separator string. If values has no members, the method returns System.String.Empty.</returns>
        public static string JoinAsString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// Filters a <see cref="IEnumerable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="source">Enumerable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the enumerable</param>
        /// <returns>Filtered or not filtered enumerable based on <paramref name="condition"/></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }

        /// <summary>
        /// Filters a <see cref="IEnumerable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="source">Enumerable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the enumerable</param>
        /// <returns>Filtered or not filtered enumerable based on <paramref name="condition"/></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }

        public static IEnumerable<T> Descendants<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> descendBy)
        {
            foreach (T value in source)
            {
                yield return value;

                foreach (T child in descendBy(value).Descendants<T>(descendBy))
                {
                    yield return child;
                }
            }
        }

        /// <summary>
        ///   Checks whether all items in the enumerable are same (Uses <see cref="object.Equals(object)" /> to check for equality)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>
        ///   Returns true if there is 0 or 1 item in the enumerable or if all items in the enumerable are same (equal to
        ///   each other) otherwise false.
        /// </returns>
        /// seealso https://stackoverflow.com/a/35839942/5483868
        public static bool AreAllSame<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            using (var enumerator = enumerable.GetEnumerator())
            {
                var toCompare = default(T);
                if (enumerator.MoveNext())
                {
                    toCompare = enumerator.Current;
                }

                while (enumerator.MoveNext())
                {
                    if (toCompare != null && !toCompare.Equals(enumerator.Current))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Selects the object in a list with the minimum value on a particular property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T FindMin<T, TValue>(this IEnumerable<T> list, Func<T, TValue> predicate) where TValue : IComparable<TValue>
        {

            T result = list.FirstOrDefault();
            if (result != null)
            {
                var bestMin = predicate(result);
                foreach (var item in list.Skip(1))
                {
                    var v = predicate(item);
                    if (v.CompareTo(bestMin) < 0)
                    {
                        bestMin = v;
                        result = item;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Selects the object in a list with the maximum value on a particular property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T FindMax<T, TValue>(this IEnumerable<T> list, Func<T, TValue> predicate) where TValue : IComparable<TValue>
        {
            T result = list.FirstOrDefault();
            if (result != null)
            {
                var bestMax = predicate(result);
                foreach (var item in list.Skip(1))
                {
                    var v = predicate(item);
                    if (v.CompareTo(bestMax) > 0)
                    {
                        bestMax = v;
                        result = item;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Converts an enumeration of groupings into a Dictionary of those groupings.
        /// </summary>
        /// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
        /// <typeparam name="TValue">Element type of the grouping and dictionary list.</typeparam>
        /// <param name="groupings">The enumeration of groupings from a GroupBy() clause.</param>
        /// <returns>A dictionary of groupings such that the key of the dictionary is TKey type and the value is List of TValue type.</returns>
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
        {
            return groupings.ToDictionary(group => group.Key, group => group.ToList());
        }

        /// <summary>
        /// Determines whether an IEnumerable contains any item
        /// </summary>
        /// <param name="enumerable">the IEnumerable</param>
        /// <returns>false if enumerable is null or contains no items</returns>
        public static bool HasItems<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return false;

            try
            {
                var enumerator = enumerable.GetEnumerator();
                if (enumerator != null && enumerator.MoveNext())
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        /// <summary>
        /// Removes items from a collection based on the condition you provide.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="Predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> RemoveDuplicates<T>(this ICollection<T> list, Func<T, int> Predicate)
        {
            var dict = new Dictionary<int, T>();

            foreach (var item in list)
            {
                if (!dict.ContainsKey(Predicate(item)))
                {
                    dict.Add(Predicate(item), item);
                }
            }

            return dict.Values.AsEnumerable();
        }

        /// <summary>
        /// Allows you to filter an IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="filterParam"></param>
        /// <returns></returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> filterParam)
        {
            return list.Where(filterParam);
        }

        /// <summary>
        /// Caches the results of an IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Cache<T>(this IEnumerable<T> source)
        {
            return CacheHelper(source.GetEnumerator());
        }

        private static IEnumerable<T> CacheHelper<T>(IEnumerator<T> source)
        {
            var isEmpty = new Lazy<bool>(() => !source.MoveNext());
            var head = new Lazy<T>(() => source.Current);
            var tail = new Lazy<IEnumerable<T>>(() => CacheHelper(source));

            return CacheHelper(isEmpty, head, tail);
        }

        private static IEnumerable<T> CacheHelper<T>(Lazy<bool> isEmpty, Lazy<T> head, Lazy<IEnumerable<T>> tail)
        {
            if (isEmpty.Value)
                yield break;

            yield return head.Value;
            foreach (var value in tail.Value)
                yield return value;
        }

        /// <summary>
        /// Enumerate each element in the enumeration and execute specified action
        /// </summary>
        /// <typeparam name="T">Type of enumeration</typeparam>
        /// <param name="enumerable">Enumerable collection</param>
        /// <param name="action">Action to perform</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
                action(item);
        }

        /// <summary>
        /// Read only collection of any enumeration
        /// </summary>
        /// <typeparam name="T">Type of enumeration</typeparam>
        /// <param name="collection">Enumerable collection</param>
        /// <returns>ReadOnlyCollection of the collection</returns>
        public static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> collection)
        {
            return (new List<T>(collection)).AsReadOnly();
        }

        /// <summary>
        /// Converts bytes collection to hexadecimal representation
        /// </summary>
        /// <param name="bytes">Bytes to convert</param>
        /// <returns>Hexadecimal representation string</returns>
        public static string ToHexString(this IEnumerable<byte> bytes)
        {
            return string.Join("", bytes.Select(b => ("0" + b.ToString("X")).Right(2)));
        }
    }
}
