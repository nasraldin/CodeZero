using System.Globalization;
using CodeZero;
using CodeZero.Configuration;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Localization
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseLocalizationServices(
        [NotNull] this IApplicationBuilder app,
        [NotNull] IConfiguration configuration)
    {
        var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? new ServiceSettings();
        var language = configuration.GetSection(nameof(Language)).Get<Language[]>();
        var supportedCultures = language?.Select(lang => new CultureInfo(lang.Culture)).ToArray()!;
        var defaultCulture = new CultureInfo[] { new CultureInfo("en"), new CultureInfo("ar") };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(serviceSettings.DefaultCulture),
            // Formatting numbers, dates, etc.
            SupportedCultures = supportedCultures ?? defaultCulture,
        });

        app.Use(async (context, next) =>
        {
            // Get client prefered language
            var userLang = context.Request.Headers[AppConsts.HeaderName.AcceptLanguage].ToString()
                            .Split(',').FirstOrDefault();

            // Set language
            var lang = supportedCultures?.FirstOrDefault(lang => lang.Name == userLang)?.Name ??
            defaultCulture.FirstOrDefault(lang => lang.Name == userLang)?.Name ??
            serviceSettings.DefaultCulture;

            context.Response.Headers.TryAdd(AppConsts.HeaderName.ContentLanguage, lang);

            // Switch app culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang!);

            await next();
        });

        return app;
    }
}