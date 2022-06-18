namespace CodeZero.Configuration;

/// <summary>
/// Represents cache configuration parameters
/// </summary>
public partial class CacheConfig
{
    /// <summary>
    /// Gets or sets the default cache time in minutes
    /// </summary>
    public int DefaultCacheTime { get; set; } = 60;

    /// <summary>
    /// Gets or sets the short term cache time in minutes
    /// </summary>
    public int ShortTermCacheTime { get; set; } = 3;

    /// <summary>
    /// Gets or sets the bundled files cache time in minutes
    /// </summary>
    public int BundledFilesCacheTime { get; set; } = 120;
}