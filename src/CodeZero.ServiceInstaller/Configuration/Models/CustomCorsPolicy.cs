namespace CodeZero.Configuration.Models;

public partial class CustomCorsPolicy
{
    public string PolicyName { get; set; } = default!;
    public bool AllowAnyHeader { get; set; }
    public bool AllowAnyMethod { get; set; }
    public bool AllowAnyOrigin { get; set; }
    public string[] Headers { get; set; } = default!;
    public string[] Methods { get; set; } = default!;
    public string[] Origins { get; set; } = default!;
    public TimeSpan? PreflightMaxAge { get; set; }
    public bool SupportsCredentials { get; set; }
}