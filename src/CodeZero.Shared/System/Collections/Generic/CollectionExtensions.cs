using System.ComponentModel;

namespace System.Collections.Generic;

/// <summary>
/// Extension methods for <see cref="ICollection{T}"/>.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class CollectionExtensions
{
    /// <summary>
    /// Checks whatever given collection object is null or has no item.
    /// </summary>
    public static bool IsNullOrEmpty<T>([CanBeNull] this ICollection<T> source)
    {
        return source is null || source.Count <= 0;
    }

    /// <summary>
    /// Adds an item to the collection if it's not already in the collection.
    /// </summary>
    /// <param name="source">The collection</param>
    /// <param name="item">Item to check and add</param>
    /// <typeparam name="T">Type of the items in the collection</typeparam>
    /// <returns>Returns True if added, returns False if not.</returns>
    public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (source.Contains(item))
        {
            return false;
        }

        source.Add(item);

        return true;
    }
}