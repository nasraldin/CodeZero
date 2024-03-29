namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Debug configuration parameters
/// </summary>
public partial class MiniProfilerConfig
{
    public string RouteBasePath { get; set; } = "/profiler";
    public string Storage { get; set; } = "MemoryCache";
    public string ConnectionString { get; set; } = default!;
}