namespace CodeZero.Configuration.Models;

public partial class QuotaExceededResponse
{
    public string ContentType { get; set; } = default!;
    public string Content { get; set; } = default!;
    public int? StatusCode { get; set; }
}