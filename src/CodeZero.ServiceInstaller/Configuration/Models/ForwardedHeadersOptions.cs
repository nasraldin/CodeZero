namespace CodeZero.Configuration.Models;

public partial class ForwardedHeadersOptions
{
    public string ForwardedForHeaderName { get; set; } = default!;
    public string ForwardedHostHeaderName { get; set; } = default!;
    public string ForwardedProtoHeaderName { get; set; } = default!;
    public string OriginalForHeaderName { get; set; } = default!;
    public string OriginalHostHeaderName { get; set; } = default!;
    public string OriginalProtoHeaderName { get; set; } = default!;
    public int? ForwardLimit { get; set; }
    public string[] KnownProxies { get; set; } = default!;
    public string[] KnownNetworks { get; set; } = default!;
    public string[] AllowedHosts { get; set; } = default!;
    public bool RequireHeaderSymmetry { get; set; }
    public bool AddActiveNetworkInterfaceToKnownNetworks { get; set; }
}