namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents CorsSettings configuration parameters
/// </summary>
public partial class CorsSettings
{
    public string DefaultCorsPolicy { get; set; } = default!;
    public List<CustomCorsPolicy> CorsPolicy { get; set; } = default!;
}