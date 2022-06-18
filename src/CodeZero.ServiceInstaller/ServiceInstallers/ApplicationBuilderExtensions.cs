using CodeZero.Middleware;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Configure ApplicationBuilder.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure HTTP request pipeline for the current path.
        /// </summary>
        public static IApplicationBuilder UseCodeZero(
            this IApplicationBuilder app,
            Action<IApplicationBuilder> configure = null!)
        {
            app.UseMiddleware<PoweredByMiddleware>();

            configure?.Invoke(app);

            return app;
        }
    }
}