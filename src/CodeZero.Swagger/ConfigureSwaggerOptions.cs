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
        var swaggerInfo = _config?.Options ?? new SwaggerInfo();

        // Register the Swagger services info
        var info = new OpenApiInfo
        {
            Title = swaggerInfo.Title,
            Version = description.ApiVersion.ToString(),
            Description = swaggerInfo.Description,
        };

        if (swaggerInfo.Contact is not null)
        {
            info.Contact = new OpenApiContact
            {
                Name = swaggerInfo.Contact?.Name,
                Email = swaggerInfo.Contact?.Email,
                Url = new Uri(swaggerInfo.Contact?.Url!)
            };
        }
        if (swaggerInfo.TermsOfService is not null)
        {
            info.TermsOfService = new Uri(swaggerInfo.TermsOfService);
        }
        if (swaggerInfo.License is not null)
        {
            info.License = new OpenApiLicense
            {
                Name = swaggerInfo.License?.Name,
                Url = new Uri(swaggerInfo.License?.Url!)
            };
        }
        if (description.IsDeprecated)
        {
            info.Description += " this API version has been deprecated.";
        }

        return info;
    }
}