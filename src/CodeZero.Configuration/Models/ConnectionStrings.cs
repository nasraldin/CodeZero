namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents ConnectionStrings configuration parameters
/// </summary>
public partial class ConnectionStrings
{
    public string DefaultConnection { get; set; } = default!;
}