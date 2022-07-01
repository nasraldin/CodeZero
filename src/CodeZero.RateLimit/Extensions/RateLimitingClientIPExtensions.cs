using AspNetCoreRateLimit;
using CodeZero.RateLimit;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add IpRateLimiting based on client IP.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddRateLimitingClientIP(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        bool.TryParse(configuration["IpRateLimiting:EnableRateLimitingRedis"], out bool enableRateLimitingRedis);

        // load general configuration from appsettings.json
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

        // load ip rules from appsettings.json
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

        // inject counter and rules stores
        services.AddInMemoryRateLimiting();

        if (enableRateLimitingRedis)
        {
            services.AddSingleton<IIpPolicyStore, RedisRateLimitPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, RedisRateLimitCounterStore>();
        }
        else
        {
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        }

        // configuration (resolvers, counter key builders)
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        return services;
    }
}
