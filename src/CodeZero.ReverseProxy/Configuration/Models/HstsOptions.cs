namespace CodeZero.Configuration.Models;

public partial class HstsOptions
{
    public int MaxAge { get; set; }
    public bool IncludeSubDomains { get; set; }
    public bool Preload { get; set; }
    public string[] ExcludedHosts { get; set; } = default!;
}
