namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Headers configuration parameters
/// </summary>
public partial class HeadersConfig
{
    public string XFrameOptions { get; set; } = default!;
    public string XContentTypeOptions { get; set; } = default!;
    public string XssProtection { get; set; } = default!;
}