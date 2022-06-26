using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds StackExchange Exceptional.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddStackExchangeExceptional(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        return services.AddExceptional(configuration.GetSection("Exceptional"));
    }
}
