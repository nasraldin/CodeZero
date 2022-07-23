namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents ApiKey configuration parameters
/// </summary>
public partial class ApiKeyConfig
{
    public string ApiKey { get; set; } = default!;
    public string HeaderName { get; set; } = default!;
    public List<AllowedClients> AllowedClients { get; set; } = default!;
}

/// <summary>
/// Represents allowed ApiKey clients configuration parameters
/// </summary>
public partial class AllowedClients
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}