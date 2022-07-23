namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Jwt configuration parameters
/// </summary>
public partial class Jwt
{
    public string Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
    public string ExpireDays { get; set; } = default!;
}