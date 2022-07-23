using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Response Compression.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddResponseCompressionConfig(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        var config = configuration.GetSection(nameof(ResponseCompressionConfig))
            .Get<ResponseCompressionConfig>();

        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = config.EnableForHttps;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();

            if (config.MimeTypes.Any())
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(config.MimeTypes);
            }
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        return services;
    }
}