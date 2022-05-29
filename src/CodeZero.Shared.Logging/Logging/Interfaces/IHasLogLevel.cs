using Microsoft.Extensions.Logging;

namespace CodeZero.Logging;

/// <summary>
/// Interface to define a <see cref="LogLevel"/> 
/// property (see <see cref="Microsoft.Extensions.Logging.LogLevel"/>).
/// </summary>
public interface IHasLogLevel
{
    /// <summary>
    /// Log severity.
    /// </summary>
    LogLevel LogLevel { get; set; }
}