using JetBrains.Annotations;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Register the MiniProfiler
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseMiniProfilerConfig([NotNull] this IApplicationBuilder app)
    {
        return app.UseMiniProfiler();
    }
}