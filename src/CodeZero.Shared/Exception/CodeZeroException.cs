using System.Runtime.Serialization;

namespace System;

/// <summary>
/// Base exception type for those are thrown 
/// by CodeZero system for app specific exceptions.
/// </summary>
[Serializable]
public class CodeZeroException : Exception
{
    private readonly string _resourceName = default!;
    private readonly IList<string> _validationErrors = default!;

    /// <inheritdoc />
    public CodeZeroException() { }

    /// <inheritdoc />
    public CodeZeroException(string message)
        : base(message)
    {
    }

    /// <inheritdoc />
    public CodeZeroException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public CodeZeroException(
        string message,
        string resourceName,
        IList<string> validationErrors)
       : base(message)
    {
        _resourceName = resourceName;
        _validationErrors = validationErrors;
    }

    public CodeZeroException(
        string message,
        string resourceName,
        IList<string> validationErrors,
        Exception innerException)
        : base(message, innerException)
    {
        _resourceName = resourceName;
        _validationErrors = validationErrors;
    }

    /// <inheritdoc />
    /// Constructor should be protected for unsealed classes, private for sealed classes.
    // (The Serializer invokes this constructor through reflection, so it can be private)
    protected CodeZeroException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Initializes a new instance of the Exception class with a specified error message.
    /// </summary>
    /// <param name="messageFormat">The exception message format.</param>
    /// <param name="args">The exception message arguments.</param>
    public CodeZeroException(string messageFormat, params object[] args)
        : base(string.Format(messageFormat, args))
    {
    }

    public string ResourceName
    {
        get { return _resourceName; }
    }

    public IList<string> ValidationErrors
    {
        get { return _validationErrors; }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        ArgumentNullException.ThrowIfNull(info);

        info.AddValue("ResourceName", ResourceName);

        // Note: if "List<T>" isn't serializable you may need to work out another
        // method of adding your list, this is just for show...
        info.AddValue("ValidationErrors", ValidationErrors, typeof(IList<string>));

        // MUST call through to the base class to let it save its own state
        base.GetObjectData(info, context);
    }
}