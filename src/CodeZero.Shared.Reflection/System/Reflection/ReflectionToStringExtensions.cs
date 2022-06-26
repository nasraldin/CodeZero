namespace System.Reflection;

/// <summary>
/// A simple helper class to print C# generic collections nicely.
/// </summary>
/// This class is copied from here:
/// https://github.com/hk1ll3r/CSharp-Collection-Print-Reflection
public static class ReflectionToStringExtensions
{
    public static bool Verbose { get; set; } = true;

    public static string ToStringExtCollection<T>(this ICollection<T> obj, int d = 0)
    {
        return (Verbose ? obj.GetType().Name + "<" + typeof(T).Name + ">" : "") + "{" + string.Join(", ", obj.Select(o => o.ToStringExt(d + 1))) + "}";
    }

    public static string ToStringExtKeyValuePair<U, V>(this KeyValuePair<U, V> kvp, int d = 0)
    {
        return (Verbose ? "KeyValuePair<" + typeof(U).Name + "," + typeof(V).Name + ">" : "") +
            "(" + kvp.Key.ToStringExt(d + 1) +
            " => " +
            kvp.Value.ToStringExt(d + 1) + ")";
    }

    public static string ToStringExt<T>(this T obj, int d = 0)
    {
        ArgumentNullException.ThrowIfNull(obj);

        string res = null!;
        Type TType = typeof(T);

        if (TType.IsGenericType &&
            (TType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)))
        {
            Type kvpType = typeof(KeyValuePair<,>)
                .MakeGenericType(TType.GetGenericArguments()[0], TType.GetGenericArguments()[1]);
            res = ReflectionToStringExtensions.ToStringExtKeyValuePair(obj.CastToReflected(kvpType), d + 1);
        }
        else
        {
            foreach (var IType in TType.GetInterfaces())
            {
                if (IType.IsGenericType &&
                    (IType.GetGenericTypeDefinition() == typeof(ICollection<>)))
                {
                    res = ReflectionToStringExtensions.ToStringExtCollection(obj.CastToReflected(IType), d + 1);
                    break;
                }
            }
        }
        if (res is null)
        {
            res = obj.ToString()!;
        }

        return res;
    }

    public static T CastTo<T>(this object o) => (T)o;

    public static dynamic CastToReflected(this object o, Type type)
    {
        var methodInfo = typeof(ReflectionToStringExtensions)
            .GetMethod(nameof(CastTo), BindingFlags.Static | BindingFlags.Public);
        var genericArguments = new[] { type };
        var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);

        return genericMethodInfo?.Invoke(null, new[] { o })!;
    }
}