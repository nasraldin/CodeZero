namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Authentication configuration parameters
/// </summary>
public partial class Authentication
{
    public string Authority { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string Scopes { get; set; } = default!;
    public bool SaveToken { get; set; } = default!;
}