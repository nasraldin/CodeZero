namespace Microsoft.Extensions.Configuration;

public class ConfigurationBuilderOptions
{
    /// <summary>
    /// Used to set user secret id for the application.
    /// </summary>
    public string UserSecretsId { get; set; } = default!;

    /// <summary>
    /// Default value: "appsettings".
    /// </summary>
    public string FileName { get; set; } = "appsettings";

    /// <summary>
    /// Default value: "appsettings.Logs".
    /// </summary>
    public string SerilogFileName { get; set; } = "Logs";

    /// <summary>
    /// Default value: "appsettings.Language".
    /// </summary>
    public string LanguageFileName { get; set; } = "Language";

    /// <summary>
    /// Environment name. Generally used "Development", "Staging" or "Production".
    /// </summary>
    public string EnvironmentName { get; set; } = default!;

    /// <summary>
    /// Base path to read the configuration file indicated by <see cref="FileName"/>.
    /// </summary>
    public string BasePath { get; set; } = default!;

    /// <summary>
    /// Prefix for the environment variables.
    /// </summary>
    public string EnvironmentVariablesPrefix { get; set; } = default!;

    /// <summary>
    /// Command line arguments.
    /// </summary>
    public string[] CommandLineArgs { get; set; } = default!;
}