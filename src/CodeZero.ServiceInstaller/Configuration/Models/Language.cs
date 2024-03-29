namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents Languages configuration parameters
/// </summary>
public partial class Language
{
    public string Name { get; set; } = default!;
    public string Culture { get; set; } = default!;
    public bool IsRtl { get; set; }
    public bool IsActive { get; set; }
    public int DisplaySequence { get; set; }
}