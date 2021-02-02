using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace CodeZero.Shared.Extensions
{
    /// <summary>
    /// Extension methods for all objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Used to simplify and beautify casting an object to a type. 
        /// </summary>
        /// <typeparam name="T">Type to be casted</typeparam>
        /// <param name="obj">Object to cast</param>
        /// <returns>Casted object</returns>
        public static T As<T>(this object obj)
            where T : class
        {
            return (T)obj;
        }

        /// <summary>
        /// Converts given object to a value or enum type using <see cref="Convert.ChangeType(object,TypeCode)"/> or <see cref="Enum.Parse(Type,string)"/> method.
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <typeparam name="T">Type of the target object</typeparam>
        /// <returns>Converted object</returns>
        public static T To<T>(this object obj)
            where T : struct
        {
            if (typeof(T) == typeof(Guid) || typeof(T) == typeof(TimeSpan))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
            }
            if (typeof(T).IsEnum)
            {
                if (Enum.IsDefined(typeof(T), obj))
                {
                    return (T)Enum.Parse(typeof(T), obj.ToString());
                }
                else
                {
                    throw new ArgumentException($"Enum type undefined '{obj}'.");
                }
            }

            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Check if an item is in a list.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="list">List of items</param>
        /// <typeparam name="T">Type of the items</typeparam>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }

        /// <summary>
        /// Can be used to conditionally perform a function
        /// on an object and return the modified or the original object.
        /// It is useful for chained calls.
        /// </summary>
        /// <param name="obj">An object</param>
        /// <param name="condition">A condition</param>
        /// <param name="func">A function that is executed only if the condition is <code>true</code></param>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <returns>
        /// Returns the modified object (by the <paramref name="func"/> if the <paramref name="condition"/> is <code>true</code>)
        /// or the original object if the <paramref name="condition"/> is <code>false</code>
        /// </returns>
        public static T If<T>(this T obj, bool condition, Func<T, T> func)
        {
            if (condition)
            {
                return func(obj);
            }

            return obj;
        }

        /// <summary>
        /// Can be used to conditionally perform an action
        /// on an object and return the original object.
        /// It is useful for chained calls on the object.
        /// </summary>
        /// <param name="obj">An object</param>
        /// <param name="condition">A condition</param>
        /// <param name="action">An action that is executed only if the condition is <code>true</code></param>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <returns>
        /// Returns the original object.
        /// </returns>
        public static T If<T>(this T obj, bool condition, Action<T> action)
        {
            if (condition)
            {
                action(obj);
            }

            return obj;
        }

        /// <summary>
        /// Converts type to another
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToAnother<T>(this IConvertible value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialisation method. NOTE: Private members are not cloned using this method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(source));
            }

            // Don't serialize a null object, simply return the default for that object
            if (source == null)
            {
                return default;
            }

            Stream stream = new MemoryStream();
            using (stream)
            {
                var serialized = JsonSerializer.Serialize(source);
                return JsonSerializer.Deserialize<T>(serialized);
            }
        }

        public static DynamicObj ToDynamicObj(this object obj)
        {
            return obj == null ? null : new DynamicObj(obj);
        }

        /// <summary>
        /// Throws an exception if the object called upon is null.
        /// </summary>
        /// <typeparam name="T">The calling class</typeparam>
        /// <param name="obj">The This object</param>
        /// <param name="text">The text to be written on the ArgumentNullException: [text] not allowed to be null</param>
        public static void ThrowIfArgumentIsNull<T>(this T obj, string text) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(text + " not allowed to be null");
        }
    }
}