using CodeZero.Configuration;
using CodeZero.Helpers;
using CodeZero.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Adds CodeZero services to the host service collection
/// Registration of the dependency in a service container.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds CodeZero services to the host service collection and let the app change
    /// the default behavior and set of features through a configure action.
    /// </summary>
    public static WebApplicationBuilder AddDefaultServices(this WebApplicationBuilder builder)
    {
        Console.WriteLine("[CodeZero] Adds ServiceInstallers...");

        // Load ServiceSettings
        var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? new();
        var debugConfig = builder.Configuration.GetSection(nameof(DebugConfig)).Get<DebugConfig>() ?? new();

        // Use non-generic Serilog.ILogger
        if (serviceSettings.EnableSerilog)
            builder.Services.AddSingleton(Log.Logger);

        // Register default framework order
        if (serviceSettings.UseLocalization)
            builder.Services.AddLocalizationServices(builder.Configuration);

        if (serviceSettings.UseMemoryCache)
            builder.Services.AddMemoryCache();

        //if (serviceSettings.EnableRedis)
        //    builder.Services.AddRedis();

        if (debugConfig.UseMiniProfiler)
            builder.Services.AddMiniProfilerConfig(builder.Configuration);

        builder.Services.AddSingleton<IPoweredByMiddlewareOptions, PoweredByMiddlewareOptions>();
        builder.Services.AddRouting(x => x.LowercaseUrls = serviceSettings.UseRoutingLowercaseUrls);
        builder.Services.AddWebEncoders();

        Console.WriteLine($"[CodeZero] Adds HttpContextHelper to runtime...");
        builder.Services.AddHttpContextAccessor();
        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddTransient(typeof(IHttpContextHelper), typeof(HttpContextHelper));
        builder.Services.TryAddSingleton<IHttpContextHelper>(
            new HttpContextHelper(builder.Services.BuildServiceProvider()
            .GetRequiredService<IHttpContextAccessor>()));

        if (serviceSettings.UseReverseProxy)
            builder.Services.AddProxy(builder.Configuration);

        if (serviceSettings.EnableResponseCompression)
            builder.Services.AddResponseCompressionConfig(builder.Configuration);

        if (serviceSettings.EnableIpRateLimiting)
            builder.Services.AddRateLimitingClientIP(builder.Configuration);

        if (serviceSettings.EnableClientRateLimiting)
            builder.Services.AddRateLimitingClientID(builder.Configuration);

        if (serviceSettings.UseAntiforgery)
            builder.Services.AddAntiforgeryConfig();

        if (serviceSettings.UseAuthentication)
            builder.Services.AddAuth(builder.Configuration);

        if (serviceSettings.UseCors)
            builder.Services.AddCorsConfig(builder.Configuration);

        if (serviceSettings.EnableSwagger)
        {
            builder.Services.AddCodeZeroApiVersioning(builder.Configuration);
            builder.Services.AddSwaggerVersioned(builder.Configuration);
        }

        if (serviceSettings.UseDataProtection)
            builder.Services.AddDataProtectionConfig(builder.Configuration);

        if (serviceSettings.UseStackExchangeExceptional)
            builder.Services.AddStackExchangeExceptional(builder.Configuration);

        if (serviceSettings.EnableContentNegotiation)
        {
            builder.Services.AddContentNegotiation();
        }
        else
        {
            builder.Services.AddControllers();
        }

        if (serviceSettings.AddMvcServices)
            builder.Services.AddMvcServices(builder.Configuration);

        return builder;
    }
}