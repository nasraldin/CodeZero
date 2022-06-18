using CodeZero;
using CodeZero.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds CORS.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddCorsConfig(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        var corsConfig = configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>();

        if (!corsConfig.CorsPolicy.Any())
            return services;

        if (corsConfig.CorsPolicy.Any(cp => string.IsNullOrEmpty(cp.PolicyName)))
            throw new CodeZeroException("PolicyName can't be null!");

        services.AddCors(options =>
        {
            corsConfig.CorsPolicy.ForEach(policy =>
            {
                options.AddPolicy(name: policy.PolicyName,
                    builder =>
                    {
                        // check for Cors allow specific
                        if (policy.AllowAnyHeader)
                            builder.AllowAnyHeader();
                        if (policy.AllowAnyMethod)
                            builder.AllowAnyMethod();
                        if (policy.AllowAnyOrigin)
                            builder.AllowAnyOrigin();

                        // not any
                        if (!policy.AllowAnyHeader && policy.Headers is not null)
                            builder.WithHeaders(policy.Headers);

                        if (!policy.AllowAnyMethod && policy.Methods is not null)
                            builder.WithHeaders(policy.Methods);

                        if (!policy.AllowAnyOrigin && policy.Origins is not null)
                            builder.WithHeaders(policy.Origins);

                        if (policy.SupportsCredentials)
                            builder.AllowCredentials();

                        if (policy.PreflightMaxAge.HasValue)
                            builder.SetPreflightMaxAge(policy.PreflightMaxAge.Value).AllowCredentials();
                    });
            });
        });

        return services;
    }
}
