using CodeZero.Configuration;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Register the Swagger generator and the Swagger UI middlewares.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <param name="provider">The <see cref="IApiVersionDescriptionProvider"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseSwaggerVersioned(
        this IApplicationBuilder app,
        IApiVersionDescriptionProvider provider,
        [NotNull] IConfiguration configuration)
    {
        var sgOptions = configuration.GetSection(nameof(SwaggerConfig)).Get<SwaggerConfig>();

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger(option => { option.RouteTemplate = sgOptions.RouteTemplate; });

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
        app.UseSwaggerUI(options =>
        {
            // build a swagger endpoint for each discovered API version
            for (var i = 0; i < provider.ApiVersionDescriptions.Count; i++)
            {
                var description = provider.ApiVersionDescriptions[i];
                options.SwaggerEndpoint($"/{sgOptions.UiEndpoint}/{description.GroupName}/{sgOptions.RoutePrefix}", description.GroupName.ToUpperInvariant());
            }

            options.DefaultModelsExpandDepth(sgOptions.DefaultModelsExpandDepth);
            options.DisplayRequestDuration();
            options.EnableValidator();
            options.EnableFilter();
            options.InjectStylesheet("/swagger-ui/custom-swagger.css");
        });

        return app;
    }
}