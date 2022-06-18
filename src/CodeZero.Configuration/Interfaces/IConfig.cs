using System.Text.Json.Serialization;

namespace CodeZero.Configuration;

/// <summary>
/// Used to retrieve configured TOptions instances.
/// </summary>
public interface IConfig<T>
{
    /// <summary>
    /// Gets a section name to load configuration
    /// </summary>
    [JsonIgnore]
    T Options { get; set; }
}