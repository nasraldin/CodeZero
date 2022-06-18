namespace CodeZero.Configuration;

/// <summary>
/// Represents FeatureManagement configuration parameters
/// </summary>
public partial class FeatureManagement
{
    public bool Seq { get; set; }
    public bool ApiKey { get; set; }
    public bool Authentication { get; set; }
    public bool Antiforgery { get; set; }
    public bool Cors { get; set; }
    public bool ReverseProxy { get; set; }
    public bool Database { get; set; }
    public bool DataProtection { get; set; }
    public bool HealthChecks { get; set; }
    public bool Localization { get; set; }
    public bool Swagger { get; set; }
    public bool RedisCache { get; set; }
    public bool MemoryCache { get; set; }
    public bool ClientRateLimiting { get; set; }
    public bool IpRateLimiting { get; set; }
    public bool RateLimitingRedis { get; set; }
    public bool StackExchangeExceptional { get; set; }
}