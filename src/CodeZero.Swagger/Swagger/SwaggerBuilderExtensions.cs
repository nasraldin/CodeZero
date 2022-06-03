using CodeZero.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Configure HTTP request pipeline for the current path.
/// </summary>
public static partial class ApplicationBuilderExtensions
{
    // Register the Swagger generator and the Swagger UI middlewares
    public static IApplicationBuilder UseSwaggerVersioned(
        this IApplicationBuilder app,
        IApiVersionDescriptionProvider provider)
    {
        var sgOptions = AppServiceLoader.Instance.Configuration.GetSection(nameof(SwaggerConfig)).Get<SwaggerConfig>();


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