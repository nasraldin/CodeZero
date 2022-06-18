namespace CodeZero.Configuration;

/// <summary>
/// Represents ApiKey configuration parameters
/// </summary>
public partial class ApiKeyConfig
{
    public string ApiKey { get; set; } = default!;
    public string HeaderName { get; set; } = default!;
    public List<AllowedClients> AllowedClients { get; set; } = default!;
}