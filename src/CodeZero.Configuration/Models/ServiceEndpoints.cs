namespace CodeZero.Configuration;

/// <summary>
/// Represents ServiceEndpoints configuration parameters
/// </summary>
public partial class ServiceEndpoints
{
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
}