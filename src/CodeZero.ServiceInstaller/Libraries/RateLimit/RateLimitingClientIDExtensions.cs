using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add IpRateLimiting based on client ID.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddRateLimitingClientID(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        // load general configuration from appsettings.json
        services.Configure<ClientRateLimitOptions>(configuration.GetSection("ClientRateLimiting"));
        // load ip rules from appsettings.json
        services.Configure<ClientRateLimitPolicies>(configuration.GetSection("ClientRateLimitPolicies"));
        // inject counter and rules stores
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        // configuration (resolvers, counter key builders)
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        return services;
    }
}