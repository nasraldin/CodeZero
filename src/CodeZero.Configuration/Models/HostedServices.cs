namespace CodeZero.Configuration;

/// <summary>
/// Represents HostedServices configuration parameters
/// </summary>
public partial class HostedServices
{
    public string ServiceName { get; set; } = default!;
    public string ServiceClientSecret { get; set; } = default!;
    public string ServiceRequestedScopes { get; set; } = default!;
    public int ServiceInterval { get; set; } = 5000;
}