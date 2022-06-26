using AspNetCoreRateLimit;
using JetBrains.Annotations;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Localization
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseRateLimitingClientIP([NotNull] this IApplicationBuilder app)
    {
        return app.UseIpRateLimiting();
    }
}