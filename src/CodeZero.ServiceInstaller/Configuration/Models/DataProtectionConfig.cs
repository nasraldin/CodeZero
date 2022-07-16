namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents DataProtection configuration parameters
/// </summary>
public partial class DataProtectionConfig
{
    public bool PersistKeysToRedis { get; set; } = default!;
    public string Path { get; set; } = "AppData";
    public string FileSystemDirectoryName { get; set; } = default!;
    public string RedisKey { get; set; } = default!;
    public KeyManagement KeyManagement { get; set; } = default!;
}