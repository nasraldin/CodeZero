namespace CodeZero.Domain.Common;

public abstract class ValueObject<T> where T : ValueObject<T>
{
    public override bool Equals(object? obj)
    {
        var valueObject = obj as T;

        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return EqualsCore(valueObject!);
    }

    private bool EqualsCore(ValueObject<T> other)
    {
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Aggregate(1,
            (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    protected static bool EqualOperator(ValueObject<T> left, ValueObject<T> right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return left?.Equals(right!) != false;
    }

    protected static bool NotEqualOperator(ValueObject<T> left, ValueObject<T> right)
    {
        return !(EqualOperator(left, right));
    }
}