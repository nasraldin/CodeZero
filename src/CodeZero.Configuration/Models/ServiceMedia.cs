namespace CodeZero.Configuration;

/// <summary>
/// Represents service media configuration parameters
/// </summary>
public partial class MediaConfig
{
    public int[] SupportedSizes { get; set; } = default!;
    public int MaxBrowserCacheDays { get; set; }
    public int MaxCacheDays { get; set; }
    public int MaxFileSize { get; set; }
    public string CdnBaseUrl { get; set; } = default!;
    public string AssetsRequestPath { get; set; } = default!;
    public string AssetsPath { get; set; } = default!;
    public bool UseTokenizedQueryString { get; set; }
    public string[] AllowedFileExtensions { get; set; } = default!;
    public string ContentSecurityPolicy { get; set; } = default!;
}