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

public partial class HstsOptions
{
    public int MaxAge { get; set; }
    public bool IncludeSubDomains { get; set; }
    public bool Preload { get; set; }
    public string[] ExcludedHosts { get; set; } = default!;
}

public partial class HttpsRedirectionOptions
{
    public int RedirectStatusCode { get; set; }
    public int? HttpsPort { get; set; }
}