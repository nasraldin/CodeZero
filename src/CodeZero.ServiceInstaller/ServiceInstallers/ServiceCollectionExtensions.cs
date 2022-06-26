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
        var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? new ServiceSettings();
        var debugConfig = builder.Configuration.GetSection(nameof(DebugConfig)).Get<DebugConfig>() ?? new DebugConfig();

        // Use non-generic Serilog.ILogger
        if (serviceSettings.EnableSerilog)
            builder.Services.AddSingleton(Log.Logger);

        // Register default framework order
        if (serviceSettings.EnableLocalization)
            builder.Services.AddLocalizationServices(builder.Configuration);

        if (serviceSettings.EnableMemoryCache)
            builder.Services.AddMemoryCache();

        //if (serviceSettings.EnableRedis)
        //    builder.Services.AddRedis();

        if (debugConfig.MiniProfilerEnabled)
            builder.Services.AddMiniProfilerConfig(builder.Configuration);

        builder.Services.AddSingleton<IPoweredByMiddlewareOptions, PoweredByMiddlewareOptions>();
        builder.Services.AddRouting(x => x.LowercaseUrls = serviceSettings.RoutingLowercaseUrls);
        builder.Services.AddWebEncoders();

        Console.WriteLine($"[CodeZero] Adds HttpContextHelper to runtime...");
        builder.Services.AddHttpContextAccessor();
        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddTransient(typeof(IHttpContextHelper), typeof(HttpContextHelper));
        builder.Services.TryAddSingleton<IHttpContextHelper>(
            new HttpContextHelper(builder.Services.BuildServiceProvider()
            .GetRequiredService<IHttpContextAccessor>()));

        if (serviceSettings.EnableReverseProxy)
            builder.Services.AddProxy(builder.Configuration);

        if (serviceSettings.EnableResponseCompression)
            builder.Services.AddResponseCompressionConfig();

        if (serviceSettings.EnableIpRateLimiting)
            builder.Services.AddRateLimitingClientIP(builder.Configuration);

        if (serviceSettings.EnableClientRateLimiting)
            builder.Services.AddRateLimitingClientID(builder.Configuration);

        if (serviceSettings.EnableAntiforgery)
            builder.Services.AddAntiforgeryConfig();

        if (serviceSettings.EnableAuthentication)
            builder.Services.AddAuth(builder.Configuration);

        if (serviceSettings.EnableCors)
            builder.Services.AddCorsConfig(builder.Configuration);

        if (serviceSettings.EnableSwagger)
        {
            builder.Services.AddCodeZeroApiVersioning(builder.Configuration);
            builder.Services.AddSwaggerVersioned(builder.Configuration);
        }

        if (serviceSettings.EnableDataProtection)
            builder.Services.AddDataProtectionConfig(builder.Configuration);

        if (serviceSettings.EnableExceptional)
            builder.Services.AddStackExchangeExceptional(builder.Configuration);

        builder.Services.AddControllers();

        if (serviceSettings.EnableContentNegotiation)
            builder.Services.AddContentNegotiation();

        if (serviceSettings.EnableMvc)
            builder.Services.AddMvcServices(builder.Configuration);

        return builder;
    }
}