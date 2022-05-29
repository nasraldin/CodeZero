namespace System;

/// <summary>
/// This attribute is used to represent a string value for a value in an enum.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class StringValueAttribute : Attribute
{
    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public string StringValue { get; protected set; }

    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public StringValueAttribute(string value)
    {
        StringValue = value;
    }
}