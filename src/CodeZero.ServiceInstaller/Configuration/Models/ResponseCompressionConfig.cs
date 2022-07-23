namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Headers configuration parameters
/// </summary>
public class ResponseCompressionConfig
{
    public bool EnableForHttps { get; set; }
    public string[] MimeTypes { get; set; } = default!;
}