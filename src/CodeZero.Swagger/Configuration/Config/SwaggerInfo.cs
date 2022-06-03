using CodeZero.Configuration.Models;

namespace CodeZero.Configuration;

/// <summary>
/// Represents SwaggerInfo configuration parameters
/// </summary>
public partial class SwaggerInfo
{
    public string Title { get; set; } = "CodeZero";
    public string Description { get; set; } = "CodeZero API Swagger";
    public Contact Contact { get; set; } = default!;
    public string TermsOfService { get; set; } = default!;
    public License License { get; set; } = default!;
}