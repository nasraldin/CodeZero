namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Azure CosmosDB Settings onfiguration parameters
/// </summary>
public partial class CosmosDBSettings
{
    public string Account { get; set; } = default!;
    public string Key { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public string ContainerName { get; set; } = default!;
}