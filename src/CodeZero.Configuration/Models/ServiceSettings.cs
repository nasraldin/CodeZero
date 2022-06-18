namespace CodeZero.Configuration;

/// <summary>
/// Represents the service settings configuration
/// </summary>
public partial class ServiceSettings
{
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
    public bool EnableReverseProxy { get; set; }
    public bool EnableSwagger { get; set; }
    public bool EnableSeq { get; set; }
    public bool EnableHealthChecks { get; set; }
    public bool EnableRedis { get; set; }
    public bool EnableIpRateLimiting { get; set; }
    public bool EnableClientRateLimiting { get; set; }
    public bool EnableRateLimitingRedis { get; set; }
    public bool EnableStackExchangeExceptional { get; set; }
}

public partial class DefaultApiVersion
{
    public int Major { get; set; } = 1;
    public int Minor { get; set; } = 0;
    public string Status { get; set; } = default!;
}