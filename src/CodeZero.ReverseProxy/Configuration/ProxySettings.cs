using CodeZero.Configuration.Models;

namespace CodeZero.Configuration;

/// <summary>
/// Represents Enforce Https configuration parameters
/// </summary>
public partial class ProxySettings
{
    public ForwardedHeadersOptions ForwardedHeadersOptions { get; set; } = default!;
    public HstsOptions HstsOptions { get; set; } = default!;
    public HttpsRedirectionOptions HttpsRedirectionOptions { get; set; } = default!;
    public string RequestBasePath { get; set; } = default!;
}