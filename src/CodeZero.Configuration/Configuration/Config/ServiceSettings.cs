using CodeZero.Configuration.Models;

namespace CodeZero.Configuration;

/// <summary>
/// Represents the service settings configuration
/// </summary>
public partial class ServiceSettings
{
    public string ServiceName { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string DefaultCulture { get; set; } = default!;
    public DefaultApiVersion DefaultApiVersion { get; set; } = default!;
    public bool InitializeDatabase { get; set; }
    public bool EnableResponseCompression { get; set; }
    public bool EnableSessionAndCookies { get; set; }
    public bool AddContentTypeHeaders { get; set; }
    public bool AddContentLengthHeaders { get; set; }
    public bool IgnoreMissingFeatureFilters { get; set; }
    public bool RoutingLowercaseUrls { get; set; }
    public bool EnableContentNegotiation { get; set; }
    public bool EnableFeatureManagement { get; set; }
}