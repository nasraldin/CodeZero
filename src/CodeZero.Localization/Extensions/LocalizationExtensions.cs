using System.Globalization;
using CodeZero.Configuration;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Localization
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    public static IServiceCollection AddLocalizationServices(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? new ServiceSettings();
        var language = configuration.GetSection(nameof(Language)).Get<Language[]>();
        var supportedCultures = language?.Select(lang => new CultureInfo(lang.Culture)).ToArray();
        var defaultCulture = new CultureInfo[] { new CultureInfo("en"), new CultureInfo("ar") };

        services.AddLocalization();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(serviceSettings.DefaultCulture);
            options.SupportedCultures = supportedCultures ?? defaultCulture;
            options.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                // Order is important, its in which order they will be evaluated
                new AcceptLanguageHeaderRequestCultureProvider(),
                //new QueryStringRequestCultureProvider(),
                //new CookieRequestCultureProvider()
            };
        });

        //services.AddControllers().AddDataAnnotationsLocalization();

        return services;
    }
}
