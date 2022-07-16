namespace CodeZero.Configuration.Models;

/// <summary>
/// Represents SwaggerInfo configuration parameters
/// </summary>
public partial class SwaggerInfo
{
    public string Title { get; set; } = AppServiceLoader.Instance.ApplicationName;
    public string Description { get; set; } = $"{AppServiceLoader.Instance.ApplicationName} API Swagger";
    public Contact Contact { get; set; } = default!;
    public string TermsOfService { get; set; } = default!;
    public License License { get; set; } = default!;
}