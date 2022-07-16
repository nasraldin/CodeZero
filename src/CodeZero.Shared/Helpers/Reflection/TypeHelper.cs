namespace CodeZero.Helpers;

/// <summary>
/// Some simple type-checking methods used internally.
/// </summary>
public static class TypeHelper
{
    public static object GetDefaultValue(Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type)!;
        }

        return null!;
    }

    public static bool IsDefaultValue([CanBeNull] object obj)
    {
        if (obj is null)
        {
            return true;
        }

        return obj.Equals(GetDefaultValue(obj.GetType()));
    }
}