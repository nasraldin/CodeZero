﻿namespace CodeZero.Configuration;

/// <summary>
/// Represents Debug configuration parameters
/// </summary>
public partial class MiniProfilerConfig
{
    public string Storage { get; set; } = default!;
    public string ConnectionString { get; set; } = default!;
}