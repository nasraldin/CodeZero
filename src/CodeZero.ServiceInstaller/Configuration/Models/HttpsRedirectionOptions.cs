namespace CodeZero.Configuration.Models;

public partial class HttpsRedirectionOptions
{
    public int RedirectStatusCode { get; set; }
    public int? HttpsPort { get; set; }
}