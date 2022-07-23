namespace CodeZero.Logging;

/// <summary>
/// Identifies a error id. The primary identifier is the "Id" property, 
/// with the "Name" property providing a short description of this type of error.
/// </summary>
public readonly struct ErrorId
{
    /// <summary>
    /// Initializes an instance of the ErrorId struct.
    /// </summary>
    /// <param name="id">The numeric identifier for this error id.</param>
    /// <param name="name">The name of this error id.</param>
    public ErrorId(int id, string name = null!)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Gets the numeric identifier for this error id.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the name of this error id.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// Two events are equal if they have the same id.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the other parameter; 
    /// otherwise, false.</returns>
    public bool Equals(ErrorId other)
    {
        if (other == default)
            return false;

        if (this == other)
            return true;

        if (!Equals(Id, other.Id)) return false;

        var otherType = other.GetType();
        var thisType = GetType();

        return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
    }

    public override bool Equals(object? obj) => obj is ErrorId objS && Equals(objS);

    public override int GetHashCode()
    {
        if (Equals(Id, default(int)))
            return base.GetHashCode();

        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"[ErrorId: {GetType().Name}] Id = {Id}";
    }

    /// <summary>
    /// Checks if two specified <see cref="ErrorId"/> instances have the
    /// same value. They are equal if they have the same Id.
    /// </summary>
    /// <param name="left">The first <see cref="ErrorId"/>.</param>
    /// <param name="right">The second <see cref="ErrorId"/>.</param>
    /// <returns>true if the objects are equal.</returns>
    public static bool operator ==(ErrorId left, ErrorId right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Checks if two specified <see cref="ErrorId"/>
    /// instances have different values.
    /// </summary>
    /// <param name="left">The first <see cref="ErrorId"/>.</param>
    /// <param name="right">The second <see cref="ErrorId"/>.</param>
    /// <returns>true if the objects are not equal.</returns>
    public static bool operator !=(ErrorId left, ErrorId right)
    {
        return !(left == right);
    }
}