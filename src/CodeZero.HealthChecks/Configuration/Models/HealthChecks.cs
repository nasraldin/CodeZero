namespace CodeZero.Configuration.Models;

public partial class HealthChecks
{
    public string Name { get; set; } = default!;
    public string Uri { get; set; } = default!;
}
