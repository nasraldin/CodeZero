using System.Runtime.Serialization;
using CodeZero.Logging;
using Microsoft.Extensions.Logging;

namespace CodeZero.Exception;

[Serializable]
public class BusinessException : CodeZeroException,
        IBusinessException,
        IHasErrorCode,
        IHasErrorDetails,
        IHasLogLevel
{
    public string Code { get; set; } = default!;
    public string Details { get; set; } = default!;
    public LogLevel LogLevel { get; set; }

    public BusinessException(
        string code = null!,
        string message = null!,
        string details = null!,
        System.Exception innerException = null!,
        LogLevel logLevel = LogLevel.Warning)
        : base(message, innerException)
    {
        Code = code;
        Details = details;
        LogLevel = logLevel;
    }

    /// <summary>
    /// Constructor for serializing.
    /// </summary>
    public BusinessException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }

    public BusinessException WithData(string name, object value)
    {
        Data[name] = value;

        return this;
    }
}
