namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Seq configuration parameters
/// </summary>
public partial class SeqOptions
{
    public string Endpoint { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
}