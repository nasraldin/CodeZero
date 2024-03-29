using System.Runtime.Serialization;

namespace CodeZero.Exception;

/// <summary>
/// This exception type is directly shown to the user.
/// </summary>
[Serializable]
public class UserFriendlyException : BusinessException, IUserFriendlyException
{
    public UserFriendlyException(
        string message,
        string code = null!,
        string details = null!,
        System.Exception innerException = null!,
        LogLevel logLevel = LogLevel.Warning)
        : base(
              code,
              message,
              details,
              innerException,
              logLevel)
    {
        Details = details;
    }

    /// <summary>
    /// Constructor for serializing.
    /// </summary>
    public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }
}