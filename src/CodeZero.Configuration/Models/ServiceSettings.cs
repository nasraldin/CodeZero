namespace CodeZero.Configuration;

/// <summary>
/// Represents the service settings configuration
/// </summary>
public partial class ServiceSettings
{
    public string DisplayName { get; set; } = AppDomain.CurrentDomain.FriendlyName;
    public string DefaultCulture { get; set; } = "en";
    public DefaultApiVersion DefaultApiVersion { get; set; } = new DefaultApiVersion();
    public bool InitializeDatabase { get; set; }
    public bool EnableResponseCompression { get; set; }
    public bool EnableSessionAndCookies { get; set; }
    //public bool AddContentTypeHeaders { get; set; }
    //public bool AddContentLengthHeaders { get; set; }
    //public bool IgnoreMissingFeatureFilters { get; set; }
    public bool RoutingLowercaseUrls { get; set; }
    public bool EnableContentNegotiation { get; set; }
    //public bool EnableFeatureManagement { get; set; }
    public bool EnableReverseProxy { get; set; }
    public bool EnableSwagger { get; set; } = true;
    public bool EnableSeq { get; set; }
    public bool EnableHealthChecks { get; set; }
    public bool EnableRedis { get; set; }
    public bool EnableMemoryCache { get; set; }
    public bool EnableIpRateLimiting { get; set; }
    public bool EnableClientRateLimiting { get; set; }
    public bool EnableRateLimitingRedis { get; set; }
    public bool EnableExceptional { get; set; }
    public bool EnableAntiforgery { get; set; }
    public bool EnableAuthentication { get; set; }
    public bool EnableApiKey { get; set; }
    public bool EnableCors { get; set; }
    public bool EnableLocalization { get; set; }
    public bool EnableDataProtection { get; set; }
    public bool EnableSerilog { get; set; }
    public bool EnableHttpsRedirection { get; set; } = true;
    public bool EnableMvc { get; set; }
}

public partial class DefaultApiVersion
{
    public int Major { get; set; } = 1;
    public int Minor { get; set; } = 0;
    public string Status { get; set; } = default!;
}