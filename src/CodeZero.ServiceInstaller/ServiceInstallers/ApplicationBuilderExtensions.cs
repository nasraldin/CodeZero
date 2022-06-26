using CodeZero.Configuration;
using CodeZero.Middleware;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
            [NotNull] IConfiguration configuration,
            Action<IApplicationBuilder> configure = null!)
        {
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? new ServiceSettings();
            var debugConfig = configuration.GetSection(nameof(DebugConfig)).Get<DebugConfig>() ?? new DebugConfig();
            var corsSettings = configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>() ?? new CorsSettings();

            var isProd = AppServiceLoader.Instance.Environment.IsProduction() || AppServiceLoader.Instance.Environment.IsStaging();
            var isDev = AppServiceLoader.Instance.Environment.IsDevelopment() || AppServiceLoader.Instance.Environment.IsDev();

            app.UseMiddleware<PoweredByMiddleware>();

            // Adds middleware for streamlined request logging.
            if (serviceSettings.EnableSerilog)
                app.UseSerilog();

            if (isProd)
            {
                // change to your exception handler!
                app.UseExceptionHandler("/Error");
            }

            if (serviceSettings.EnableHealthChecks)
                app.UseCodeZeroHealthChecks(configuration);

            // Register the IpRateLimiting
            if (serviceSettings.EnableIpRateLimiting)
                app.UseRateLimitingClientIP();

            if (serviceSettings.EnableClientRateLimiting)
                app.UseRateLimitingClientID();

            if (serviceSettings.EnableReverseProxy)
                app.UseProxy(configuration);

            if (serviceSettings.EnableHttpsRedirection)
                app.UseHttpsRedirection();

            if (serviceSettings.EnableResponseCompression)
                app.UseResponseCompression();

            if (debugConfig.MiniProfilerEnabled)
                app.UseMiniProfilerConfig();

            // Register the StackExchange Exceptional.
            if (serviceSettings.EnableExceptional)
                app.UseStackExchangeExceptional();

            if (serviceSettings.EnableLocalization)
                app.UseLocalizationServices(configuration);

            app.UseStaticFiles();
            app.UseHeaders(configuration);

            if (serviceSettings.EnableSwagger)
            {
                // Enable middleware to serve generated Swagger with wersioning as a JSON endpoint.
                app.UseSwaggerVersioned(configuration);
            }

            app.UseRouting();

            if (serviceSettings.EnableCors && !string.IsNullOrEmpty(corsSettings.DefaultCorsPolicy))
                app.UseCors(corsSettings.DefaultCorsPolicy);

            if (serviceSettings.EnableSessionAndCookies)
                app.UseCookiePolicy().UseSession();

            if (serviceSettings.EnableAuthentication || serviceSettings.EnableApiKey)
            {
                // Adds authenticaton middleware to the pipeline 
                // so authentication will be performed automatically on each request to host
                app.UseAuthentication();

                // Adds authorization middleware to the pipeline 
                // to make sure the Api endpoint cannot be accessed by anonymous clients
                app.UseAuthorization();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Keep as it in last call
            if (isDev)
            {
                app.UseDeveloperExceptionPage();
            }

            configure?.Invoke(app);

            return app;
        }
    }
}