namespace CodeZero.Configuration;

/// <summary>
/// Represents HostedServices configuration parameters
/// </summary>
public partial class HostedServices
{
    public string HostedServiceName { get; set; } = default!;
    public string HostedServiceClientSecret { get; set; } = default!;
    public string HostedServiceRequestedScopes { get; set; } = default!;
    public int HostedServiceInterval { get; set; } = 5000;
}