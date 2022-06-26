namespace CodeZero.Configuration.Models;

public partial class Checks
{
    public bool Database { get; set; }
    public bool DbContext { get; set; }
    public bool Redis { get; set; }
    public bool Seq { get; set; }
}