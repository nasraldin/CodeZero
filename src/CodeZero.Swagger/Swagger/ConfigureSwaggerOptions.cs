using CodeZero.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeZero.Swagger;

/// <summary>
/// Configures the Swagger generation options.
/// </summary>
/// <remarks>
/// This allows API versioning to define a Swagger document per API version 
/// after the <see cref="IApiVersionDescriptionProvider" /> service has been resolved from the service container.
/// </remarks>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly IConfig<SwaggerInfo> _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions" /> class.
    /// </summary>
    /// <param name="provider">
    /// The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.
    /// </param>
    /// <param name="config">
    /// The <see cref="IConfig{T}">config</see> used to generate AppConfig.
    /// </param>
    public ConfigureSwaggerOptions(
        IApiVersionDescriptionProvider provider,
        IConfig<SwaggerInfo> config)
    {
        _provider = provider;
        _config = config;
    }

    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        // Add a swagger document for each discovered API version
        // Note: you might choose to skip or document deprecated API versions differently
        foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        // Register the Swagger services info
        var info = new OpenApiInfo
        {
            Title = _config.Options.Title,
            Version = description.ApiVersion.ToString(),
            Description = _config.Options.Description,
            Contact = new OpenApiContact
            {
                Name = _config.Options.Contact.Name,
                Email = _config.Options.Contact.Email,
                Url = new Uri(_config.Options.Contact.Url)
            },
            TermsOfService = new Uri(_config.Options.TermsOfService),
            License = new OpenApiLicense
            {
                Name = _config.Options.License.Name,
                Url = new Uri(_config.Options.License.Url)
            }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}