namespace System.Collections.Generic;

/// <summary>
/// Extension methods for <see cref="IDictionary{TKey, T}"/>.
/// </summary>
public static class DictionaryExtensions
{
    public static T? GetValueOrDefault<TKey, T>(this IDictionary<TKey, T> source, TKey key)
        where TKey : notnull
    {
        return GetValueOrDefault(source, key, defaultValue: default);
    }

    public static T? GetValueOrDefault<TKey, T>(
        this IDictionary<TKey, T> source,
        TKey key,
        T? defaultValue)
        where TKey : notnull
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (key is null) throw new ArgumentNullException(nameof(key));
        if (source.TryGetValue(key, out var item))
        {
            return item;
        }

        return defaultValue;
    }

    /// <summary>
    /// Gets a value from the dictionary with given key. Returns default value if can not find.
    /// </summary>
    /// <param name="dictionary">Dictionary to check and get</param>
    /// <param name="key">Key to find the value</param>
    /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    /// <returns>Value if found, default if can not found.</returns>
    public static TValue GetOrAdd<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        Func<TKey, TValue> factory)
    {
        if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));
        if (key is null) throw new ArgumentNullException(nameof(key));
        if (dictionary.TryGetValue(key, out var obj))
        {
            return obj;
        }

        return dictionary[key] = factory(key);
    }

    /// <summary>
    /// Gets a value from the dictionary with given key. Returns default value if can not find.
    /// </summary>
    /// <param name="dictionary">Dictionary to check and get</param>
    /// <param name="key">Key to find the value</param>
    /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
    /// <typeparam name="TKey">Type of the key</typeparam>
    /// <typeparam name="TValue">Type of the value</typeparam>
    /// <returns>Value if found, default if can not found.</returns>
    public static TValue GetOrAdd<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        Func<TValue> factory)
    {
        return dictionary.GetOrAdd(key, k => factory());
    }
}