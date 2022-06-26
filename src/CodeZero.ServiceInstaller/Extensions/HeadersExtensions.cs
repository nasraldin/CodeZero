using CodeZero;
using CodeZero.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Register the Headers
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseHeaders(
        [NotNull] this IApplicationBuilder app,
        [NotNull] IConfiguration configuration)
    {
        var headersConfig = configuration.GetSection(nameof(HeadersConfig)).Get<HeadersConfig>();

        app.Use(async (context, next) =>
        {
            if (headersConfig is not null)
            {
                context.Response.Headers.Add(AppConsts.HeaderName.XFrameOptions, headersConfig.XFrameOptions);
                context.Response.Headers.Add(AppConsts.HeaderName.XssProtection, headersConfig.XssProtection);
                context.Response.Headers.Add(AppConsts.HeaderName.XContentTypeOptions, headersConfig.XContentTypeOptions);
            }

            // Remove version discloser
            context.Response.Headers.Remove("X-AspNet-Version");
            context.Response.Headers.Remove("X-AspNetMvc-Version");
            context.Response.Headers.Remove("Server");

            await next();
        });

        return app;
    }
}