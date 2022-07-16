namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents PaginationOptions configuration parameters
/// </summary>
public partial class PaginationOptions
{
    public int DefaultPageSize { get; set; }
    public int DefaultMaxPageCount { get; set; }
}