using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeZero.Shared.Extensions.Collections
{
    /// <summary>
    /// Extension methods for <see cref="IList{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        public static void InsertRange<T>(this IList<T> source, int index, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                source.Insert(index++, item);
            }
        }

        public static int FindIndex<T>(this IList<T> source, Predicate<T> selector)
        {
            for (var i = 0; i < source.Count; ++i)
            {
                if (selector(source[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static void AddFirst<T>(this IList<T> source, T item)
        {
            source.Insert(0, item);
        }

        public static void AddLast<T>(this IList<T> source, T item)
        {
            source.Insert(source.Count, item);
        }

        public static void InsertAfter<T>(this IList<T> source, T existingItem, T item)
        {
            var index = source.IndexOf(existingItem);
            if (index < 0)
            {
                source.AddFirst(item);
                return;
            }

            source.Insert(index + 1, item);
        }

        public static void InsertAfter<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            var index = source.FindIndex(selector);
            if (index < 0)
            {
                source.AddFirst(item);
                return;
            }

            source.Insert(index + 1, item);
        }

        public static void InsertBefore<T>(this IList<T> source, T existingItem, T item)
        {
            var index = source.IndexOf(existingItem);
            if (index < 0)
            {
                source.AddLast(item);
                return;
            }

            source.Insert(index, item);
        }

        public static void InsertBefore<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            var index = source.FindIndex(selector);
            if (index < 0)
            {
                source.AddLast(item);
                return;
            }

            source.Insert(index, item);
        }

        public static void ReplaceWhile<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (selector(source[i]))
                {
                    source[i] = item;
                }
            }
        }

        public static void ReplaceWhile<T>(this IList<T> source, Predicate<T> selector, Func<T, T> itemFactory)
        {
            for (int i = 0; i < source.Count; i++)
            {
                var item = source[i];
                if (selector(item))
                {
                    source[i] = itemFactory(item);
                }
            }
        }

        public static void ReplaceOne<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (selector(source[i]))
                {
                    source[i] = item;
                    return;
                }
            }
        }

        public static void ReplaceOne<T>(this IList<T> source, Predicate<T> selector, Func<T, T> itemFactory)
        {
            for (int i = 0; i < source.Count; i++)
            {
                var item = source[i];
                if (selector(item))
                {
                    source[i] = itemFactory(item);
                    return;
                }
            }
        }

        public static void ReplaceOne<T>(this IList<T> source, T item, T replaceWith)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (Comparer<T>.Default.Compare(source[i], item) == 0)
                {
                    source[i] = replaceWith;
                    return;
                }
            }
        }

        public static void MoveItem<T>(this List<T> source, Predicate<T> selector, int targetIndex)
        {
            if (!targetIndex.IsBetween(0, source.Count - 1))
            {
                throw new IndexOutOfRangeException("targetIndex should be between 0 and " + (source.Count - 1));
            }

            var currentIndex = source.FindIndex(0, selector);
            if (currentIndex == targetIndex)
            {
                return;
            }

            var item = source[currentIndex];
            source.RemoveAt(currentIndex);
            source.Insert(targetIndex, item);
        }

        [NotNull]
        public static T GetOrAdd<T>([NotNull] this IList<T> source, Func<T, bool> selector, Func<T> factory)
        {
            Check.NotNull(source, nameof(source));

            var item = source.FirstOrDefault(selector);

            if (item == null)
            {
                item = factory();
                source.Add(item);
            }

            return item;
        }

        /// <summary>
        /// Sort a list by a topological sorting, which consider their dependencies.
        /// </summary>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <param name="source">A list of objects to sort</param>
        /// <param name="getDependencies">Function to resolve the dependencies</param>
        /// <param name="comparer">Equality comparer for dependencies </param>
        /// <returns>
        /// Returns a new list ordered by dependencies.
        /// If A depends on B, then B will come before than A in the resulting list.
        /// </returns>
        public static List<T> SortByDependencies<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>> getDependencies,
            IEqualityComparer<T> comparer = null)
        {
            /* See: http://www.codeproject.com/Articles/869059/Topological-sorting-in-Csharp
             *      http://en.wikipedia.org/wiki/Topological_sorting
             */

            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>(comparer);

            foreach (var item in source)
            {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <param name="item">Item to resolve</param>
        /// <param name="getDependencies">Function to resolve the dependencies</param>
        /// <param name="sorted">List with the sortet items</param>
        /// <param name="visited">Dictionary with the visited items</param>
        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted,
            Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found! Item: " + item);
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }

        /// <summary>
        /// Insert an item to a sorted List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <example>l.InsertSorted(3)</example>
        public static int InsertSorted<T>(this IList<T> source, T value) where T : IComparable<T>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            for (int i = 0; i < source.Count; i++)
            {
                if (value.CompareTo(source[i]) < 0)
                {
                    source.Insert(i, value);
                    return i;
                }
            }
            source.Add(value);
            return source.Count - 1;
        }

        /// <summary>
        /// Insert an item to a sorted List with Comparer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static int InsertSorted<T>(this IList<T> source, T value, IComparer<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            for (int i = 0; i < source.Count; i++)
            {
                if (comparison.Compare(value, source[i]) < 0)
                {
                    source.Insert(i, value);
                    return i;
                }
            }
            source.Add(value);
            return source.Count - 1;
        }

        /// <summary>
        /// Insert an item to a sorted List with CompareTo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        /// l.InsertSorted(7, (x, y) => x.CompareTo(y))
        public static int InsertSorted<T>(this IList<T> source, T value, Comparison<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            for (int i = 0; i < source.Count; i++)
            {
                if (comparison(value, source[i]) < 0)
                {
                    source.Insert(i, value);
                    return i;
                }
            }
            source.Add(value);
            return source.Count - 1;
        }

        /// <summary>
        /// Allows you to chain .Add method typesafe
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TList Push<TList, TItem>(this TList list, TItem item) where TList : IList<TItem>
        {
            list.Add(item);
            return list;
        }

        /// <summary>
        /// Determines whether the given IList object [is null or empty].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if the given IList object [is null or empty]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty<T>(this IList<T> obj)
        {
            return obj == null || obj.Count == 0;
        }

        /// <summary>
        /// lambda expression to replace the first item that satisfies the condition.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="source"></param>
        /// <param name="replacement"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Replace<TSource, Tkey>(this IList<TSource> source, TSource replacement, Func<TSource, Tkey> selector)
        {
            foreach (var item in source)
            {
                var key = selector(item);

                if (key.Equals(true))
                {
                    int index = source.IndexOf(item);
                    source.Remove(item);
                    source.Insert(index, replacement);
                    break;
                }
            }
            return source;
        }

        /// <summary>
        /// Allows to clone an etire generic list of cloneable items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
