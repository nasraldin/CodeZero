namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents the service settings configuration
/// </summary>
public partial class ServiceSettings
{
    public string DefaultCulture { get; set; } = "en";
    public DefaultApiVersion DefaultApiVersion { get; set; } = new();
    public bool InitializeDatabase { get; set; }
    public bool UseAuthentication { get; set; }
    public bool UseApiKey { get; set; }
    public bool UseLocalization { get; set; }
    public bool UseHttpsRedirection { get; set; }
    public bool UseRoutingLowercaseUrls { get; set; }
    public bool UseDataProtection { get; set; }
    public bool UseReverseProxy { get; set; }
    public bool UseCors { get; set; }
    public bool UseRedis { get; set; }
    public bool UseMemoryCache { get; set; }
    public bool UseAntiforgery { get; set; }
    public bool UseStackExchangeExceptional { get; set; }
    public bool AddMvcServices { get; set; }
    public bool EnableResponseCompression { get; set; }
    public bool EnableContentNegotiation { get; set; }
    public bool EnableIpRateLimiting { get; set; }
    public bool EnableClientRateLimiting { get; set; }
    public bool EnableSwagger { get; set; }
    public bool EnableSerilog { get; set; }
    public bool EnableSeq { get; set; }
}

public class DefaultApiVersion
{
    public int Major { get; set; } = 1;
    public int Minor { get; set; } = 0;
    public string Status { get; set; } = default!;
}