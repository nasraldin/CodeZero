using CodeZero;
using CodeZero.ApiVersioning;
using CodeZero.Configuration;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds API versioning, API explorer service.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="config">The <see cref="IConfiguration"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddCodeZeroApiVersioning(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration config)
    {
        services.AddTransient<IRequestedApiVersion, HttpContextRequestedApiVersion>();
        services.AddSingleton<IRequestedApiVersion>(NullRequestedApiVersion.Instance);

        var apiVer = config.GetSection(nameof(DefaultApiVersion)).Get<DefaultApiVersion>();

        // Add API versioning
        services.AddApiVersioning(options =>
        {
            // Specify the default api version
            options.DefaultApiVersion = new ApiVersion(apiVer.Major, apiVer.Minor, apiVer.Status);

            // Reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
            options.ReportApiVersions = true;

            // Assume that the caller wants the default version if they don't specify
            options.AssumeDefaultVersionWhenUnspecified = true;

            // Read the version number from the accept header 
            ////options.ApiVersionReader = new MediaTypeApiVersionReader();

            // Read the version number from the header 
            options.ApiVersionReader = new HeaderApiVersionReader(AppConsts.HeaderName.ApiVersion);
        });

        // Adds an API explorer
        services.AddVersionedApiExplorer(options =>
        {
            options.DefaultApiVersion = new ApiVersion(apiVer.Major, apiVer.Minor, apiVer.Status);

            // Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // Note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            options.AssumeDefaultVersionWhenUnspecified = true;

            // Note: this option is only necessary when versioning by url segment. 
            // the SubstitutionFormat can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}