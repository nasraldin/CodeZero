﻿using CodeZero.Configuration.Models;

namespace CodeZero.Configuration;

/// <summary>
/// Represents HealthChecksUI configuration parameters
/// </summary>
public partial class HealthChecksUI
{
    public string UiEndpoint { get; set; } = "healthchecks-ui";
    public string HeaderText { get; set; } = "CodeZero - Health Checks Status";
    public string StorageConnectionString { get; set; } = default!;
    public bool EnableDatabaseStorage { get; set; } = false;
    public List<HealthChecks> HealthChecks { get; set; } = default!;
}