using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Register the Antiforgery.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseAntiforgery([NotNull] this IApplicationBuilder app)
    {
        IAntiforgery antiforgery = app.ApplicationServices.GetRequiredService<IAntiforgery>();

        return app.Use(next => context =>
        {
            string path = context.Request.Path.Value!;

            if (string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase))
            {
                // The request token can be sent as a JavaScript-readable cookie, 
                // and Angular uses it by default.
                var tokens = antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append(AppConst.HeaderName.XsrfToken,
                    tokens.RequestToken!, new CookieOptions() { HttpOnly = false });
            }

            return next(context);
        });
    }
}