using CodeZero;
using CodeZero.Configuration;
using CodeZero.Swagger;
using CodeZero.Swagger.Filters;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add OpenAPI and Swagger DI services and configure documents and 
    /// register the Swagger generator and the Swagger UI middlewares.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <param name="setupAction">Configures the Swagger generation options 
    /// <see cref="Action{SwaggerGenOptions}"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddSwaggerVersioned(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration,
        Action<SwaggerGenOptions> setupAction = null!)
    {
        var swaggerConfig = configuration.GetSection(nameof(SwaggerConfig)).Get<SwaggerConfig>() ?? new SwaggerConfig();
        var apiKeyConfig = configuration.GetSection(nameof(ApiKeyConfig)).Get<ApiKeyConfig>();
        // Convert Scopes List to Dictionary (a map of key and value)
        //var scopes = swaggerConfig.Scopes.ToDictionary(sn => sn.ScopeName, sd => sd.ShortDescription);

        // Add Swagger info
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.FirstOrDefault());

            if (!string.IsNullOrEmpty(XmlCommentsFilePath))
                options.IncludeXmlComments(XmlCommentsFilePath);

            options.EnableAnnotations();
            // Add a custom operation filter which sets default values
            options.OperationFilter<SwaggerDefaultValues>();
            options.OperationFilter<AcceptLanguageHeader>();
            options.OperationFilter<RemoveVersionFromParameter>();
            options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

            // Uses full schema names to avoid v1/v2/v3 schema collisions
            // see: https://github.com/domaindrivendev/Swashbuckle/issues/442
            //options.CustomSchemaIds(x => x.FullName);

            if (swaggerConfig.EnableAuthentication)
            {
                options.AddSecurityDefinition(AppConsts.AuthSchemes.Bearer, new OpenApiSecurityScheme
                {
                    Description = "Input the JWT like: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = AppConsts.AuthSchemes.Bearer,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        swaggerConfig.Scopes?.Select(s=>s.ScopeName).ToList()
                        //new string[] {} // ex: { "readAccess", "writeAccess" }
                    }
                });
            }

            if (swaggerConfig.EnableApiKey)
            {
                if (apiKeyConfig is null)
                {
                    throw new CodeZeroException($"Configure {nameof(ApiKeyConfig)} settings in appsettings.Environment.json");
                }

                options.AddSecurityDefinition(apiKeyConfig.HeaderName, new OpenApiSecurityScheme
                {
                    Description = $"ApiKey needed to access the endpoints. {apiKeyConfig.HeaderName}: Your_API_Key",
                    Type = SecuritySchemeType.ApiKey,
                    Name = apiKeyConfig.HeaderName,
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = apiKeyConfig.HeaderName,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = apiKeyConfig.HeaderName
                            },
                        },
                        swaggerConfig.Scopes?.Select(s=>s.ScopeName).ToList()
                    }
                });
            }

            options.OperationFilter<SecurityRequirements>();

            // Adds fluent validation rules to swagger
            // options.AddFluentValidationRules();

            // Add filters to fix enums
            // use by default:
            //options.AddEnumsWithValuesFixFilters();

            // or configured:
            //options.AddEnumsWithValuesFixFilters(services, o =>
            //{
            //    // add schema filter to fix enums (add 'x-enumNames' for NSwag) in schema
            //    o.ApplySchemaFilter = true;

            //    // add parameter filter to fix enums (add 'x-enumNames' for NSwag) in schema parameters
            //    o.ApplyParameterFilter = true;

            //    // add document filter to fix enums displaying in swagger document
            //    o.ApplyDocumentFilter = true;

            //    // add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' for schema extensions) for applied filters
            //    o.IncludeDescriptions = true;

            //    // get descriptions from DescriptionAttribute then from xml-comments
            //    o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;

            //    // get descriptions from xml-file comments on the specified path
            //    // should use "options.IncludeXmlComments(xmlFilePath);" before
            //    o.IncludeXmlCommentsFrom(xmlFilePath);
            //    // the same for another xml-files...
            //});

            // Use method name as operationId
            //options.CustomOperationIds(apiDesc =>
            //{
            //    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
            //});

            // Ensure the routes are added to the right Swagger doc
            //options.DocInclusionPredicate((docName, apiDesc) =>
            //{
            //    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

            //    var versions = methodInfo.DeclaringType
            //    .GetCustomAttributes(true)
            //    .OfType<ApiVersionAttribute>()
            //    .SelectMany(attr => attr.Versions);

            //    var maps = methodInfo
            //    .GetCustomAttributes(true)
            //    .OfType<MapToApiVersionAttribute>()
            //    .SelectMany(attr => attr.Versions)
            //    .ToArray();

            //    return versions.Any(v => $"v{v}" == docName) && (!maps.Any() || maps.Any(v => $"v{v}" == docName));
            //});

            // Omit Obsolete Operations and/or Schema Properties
            //options.IgnoreObsoleteActions();
            //options.IgnoreObsoleteProperties();

            setupAction?.Invoke(options);
        });

        return services;
    }

    // Get the comments path for the Swagger JSON and UI.
    private static string XmlCommentsFilePath
    {
        get
        {
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{AppDomain.CurrentDomain.FriendlyName}.xml";
            if (File.Exists(xmlFile))
                return Path.Combine(AppContext.BaseDirectory, xmlFile);
            return string.Empty;
        }
    }
}
