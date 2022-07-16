namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Integrating External Services configuration parameters
/// </summary>
public partial class ExternalServices
{
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
}