using CodeZero.Configuration.Models;

namespace CodeZero.HealthChecks.Configuration;

/// <summary>
/// Represents HealthChecksUI configuration parameters
/// </summary>
public partial class HealthChecksUI
{
    public string UiEndpoint { get; set; } = "healthchecks-ui";
    public string HeaderText { get; set; } = "CodeZero - Health Checks Status";
    public string StorageConnectionString { get; set; } = default!;
    public bool EnableDatabaseStorage { get; set; } = false;
    public List<CodeZero.Configuration.Models.HealthChecks> HealthChecks { get; set; } = default!;
    public Checks Checks { get; set; } = default!;
}