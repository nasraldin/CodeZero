using CodeZero.HealthChecks.Configuration;
using HealthChecks.UI.Client;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Register the Swagger generator and the Swagger UI middlewares.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseCodeZeroHealthChecks(
        this IApplicationBuilder app,
        [NotNull] IConfiguration configuration)
    {
        var hcsUIConfig = configuration.GetSection(nameof(HealthChecksUI)).Get<HealthChecksUI>();

        hcsUIConfig.HealthChecks?.ForEach(item =>
        {
            // HealthCheck middleware
            app.UseHealthChecks(item.Uri, new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        });

        // HealthCheck UI default is /healthchecks-ui/
        app.UseHealthChecksUI(opt =>
        {
            opt.UIPath = hcsUIConfig.UiEndpoint;
            opt.AddCustomStylesheet($"wwwroot/healthchecks-ui/custom-healthchecks.css");
        });

        return app;
    }
}