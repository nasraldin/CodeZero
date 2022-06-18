using System.IO.Compression;
using CodeZero;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Response Compression.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddResponseCompressionConfig([NotNull] this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
            {
                AppConsts.MimeTypes.JSON,
                AppConsts.MimeTypes.Image_JPEG,
                AppConsts.MimeTypes.Image_PNG,
                AppConsts.MimeTypes.Image_SVG,
                AppConsts.MimeTypes.Multipart_Mixed,
                AppConsts.MimeTypes.Multipart_FormData,
                AppConsts.MimeTypes.Text,
                AppConsts.MimeTypes.Text_UTF8,
                AppConsts.MimeTypes.Text_CSS,
                AppConsts.MimeTypes.Text_CSV,
                AppConsts.MimeTypes.Text_HTML
            });
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