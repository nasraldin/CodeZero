namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Redis configuration parameters
/// </summary>
public partial class RedisConfig
{
    /// <summary>
    /// Gets or sets Redis connection string. Used when Redis is enabled
    /// </summary>
    public string ConnectionString { get; set; } = default!;

    /// <summary>
    /// Gets or sets a specific redis database; If you need to use 
    /// a specific redis database, just set its number here. 
    /// set NULL if should use the different database 
    /// for each data type (used by default)
    /// </summary>
    public int? DatabaseId { get; set; } = null;

    /// <summary>
    /// Gets or sets a value indicating whether we should ignore 
    /// Redis timeout exception (Enabling this setting increases 
    /// cache stability but may decrease site performance)
    /// </summary>
    public bool IgnoreTimeoutException { get; set; } = false;
}