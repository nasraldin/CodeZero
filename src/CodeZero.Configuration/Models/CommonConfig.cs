using System.Text.Json.Serialization;

namespace CodeZero.Configuration;

/// <summary>
/// Represents common configuration parameters
/// </summary>
public partial class CommonConfig
{
    /// <summary>
    /// Gets a section name to load configuration
    /// </summary>
    [JsonIgnore]
    public string Name => GetType().Name;
}