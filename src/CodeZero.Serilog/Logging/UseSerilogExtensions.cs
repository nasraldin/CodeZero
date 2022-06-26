using JetBrains.Annotations;
using Serilog;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Register the Serilog.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseSerilog([NotNull] this IApplicationBuilder app)
    {
        // Adds middleware for streamlined request logging.
        return app.UseSerilogRequestLogging();
    }
}