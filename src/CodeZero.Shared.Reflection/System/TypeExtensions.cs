using System.Reflection;
using JetBrains.Annotations;

namespace System;

public static class TypeExtensions
{
    public static Assembly GetAssembly(this Type type)
    {
        return type.GetTypeInfo().Assembly;
    }

    public static MethodInfo GetMethod(
        this Type type,
        string methodName,
        int pParametersCount = 0,
        int pGenericArgumentsCount = 0)
    {
        return type
            .GetMethods()
            .Where(m => m.Name == methodName).AsEnumerable()
            .Select(m => new
            {
                Method = m,
                Params = m.GetParameters(),
                Args = m.GetGenericArguments()
            })
            .Where(x => x.Params.Length == pParametersCount && x.Args.Length == pGenericArgumentsCount)
            .Select(x => x.Method)
            .First();
    }

    public static string GetFullNameWithAssemblyName(this Type type)
    {
        return type.FullName + ", " + type.Assembly.GetName().Name;
    }

    /// <summary>
    /// Determines whether an instance of this type can be assigned to
    /// an instance of the <typeparamref name="TTarget"></typeparamref>.
    /// Internally uses <see cref="Type.IsAssignableFrom"/>.
    /// </summary>
    /// <typeparam name="TTarget">Target type</typeparam> (as reverse).
    public static bool IsAssignableTo<TTarget>([NotNull] this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsAssignableTo(typeof(TTarget));
    }

    /// <summary>
    /// Determines whether an instance of this type can be assigned to
    /// an instance of the <paramref name="targetType"></paramref>.
    ///
    /// Internally uses <see cref="Type.IsAssignableFrom"/> (as reverse).
    /// </summary>
    /// <param name="type">this type</param>
    /// <param name="targetType">Target type</param>
    public static bool IsAssignableTo([NotNull] this Type type, [NotNull] Type targetType)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(targetType);

        return targetType.IsAssignableFrom(type);
    }

    /// <summary>
    /// Gets all base classes of this type.
    /// </summary>
    /// <param name="type">The type to get its base classes.</param>
    /// <param name="includeObject">True, to include the standard 
    /// <see cref="object"/> type in the returned array.</param>
    public static Type[] GetBaseClasses([NotNull] this Type type, bool includeObject = true)
    {
        ArgumentNullException.ThrowIfNull(type);

        var types = new List<Type>();
        AddTypeAndBaseTypesRecursively(types, type.BaseType!, includeObject);

        return types.ToArray();
    }

    private static void AddTypeAndBaseTypesRecursively(
        [NotNull] List<Type> types,
        [CanBeNull] Type type,
        bool includeObject)
    {
        ArgumentNullException.ThrowIfNull(types);

        if (type is null)
        {
            return;
        }
        if (!includeObject && type == typeof(object))
        {
            return;
        }

        AddTypeAndBaseTypesRecursively(types, type.BaseType!, includeObject);
        types.Add(type);
    }
}