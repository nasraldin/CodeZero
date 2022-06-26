namespace System;

public static class NullableExtensions
{
    /// <summary>
    /// Check if index.HasValue and index.Value == value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool HasValueAndEquals<T>(this T? source, T target)
        where T : struct
    {
        return source.HasValue && source.Value.Equals(target);
    }

    /// <summary>
    /// Check if index.HasValue and index.Value == value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool HasValueAndEquals<T>(this T? source, T? target)
        where T : struct
    {
        return source.HasValue && source.Value.Equals(target);
    }
}