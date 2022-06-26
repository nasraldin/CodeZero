namespace CodeZero.Configuration;

/// <summary>
/// Represents Debug configuration parameters
/// </summary>
public partial class DebugConfig
{
    public bool CaptureStartupErrors { get; set; }
    public bool DetailedErrorsKey { get; set; }
    public bool SensitiveDataLogging { get; set; }
    public bool EnableDetailedErrors { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether to display the full error in production environment.
    /// It's ignored (always enabled) in development environment
    /// </summary>
    public bool DisplayFullErrorStack { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates whether to use MiniProfiler services
    /// </summary>
    public bool MiniProfilerEnabled { get; set; }
    public bool SerilogSelfLog { get; set; }
}