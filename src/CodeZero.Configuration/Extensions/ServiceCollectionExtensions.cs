using CodeZero.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.FeatureManagement;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Adds CodeZero Configuration services to the host service collection
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds CodeZero <see cref="IAppServiceLoader"/> services to the host service collection
    /// and let the app change the default behavior and set of features through a configure action.
    /// </summary>
    public static WebApplicationBuilder AddDefaultConfiguration(this WebApplicationBuilder builder)
    {
        Console.WriteLine("[CodeZero] Adds default configuration services to the host service collection...");

        // Add Load configuration from appsettings.json
        builder.Services.AddOptions();

        // Register Core configuration
        builder.Services.AddTransient(typeof(IAppServiceLoader), typeof(AppServiceLoader));
        builder.Services.TryAddSingleton<IAppServiceLoader>(new AppServiceLoader(builder.Environment, builder.Configuration));
        builder.Services.AddTransient(typeof(IConfig<>), typeof(Config<>));

        bool.TryParse(builder.Configuration["ServiceSettings:EnableFeatureManagement"], out bool checkFeatureManagerIsEnable);

        Console.WriteLine($"[CodeZero] Check Feature Management is Enable: {checkFeatureManagerIsEnable}");

        if (checkFeatureManagerIsEnable)
        {
            // Register FeatureManager
            builder.Services.AddFeatureManagement(builder.Configuration);
            var featureManager = builder.Services.BuildServiceProvider().GetRequiredService<IFeatureManager>();

            // Load FeatureManager
            AppServiceLoader.Instance.FeatureManager = featureManager;

            bool.TryParse(builder.Configuration["ServiceSettings.IgnoreMissingFeatureFilters"], out bool ignoreMissingFeatureFilters);

            // Disabled feature filter hasn't been registered
            builder.Services.Configure<FeatureManagementOptions>(options =>
            {
                options.IgnoreMissingFeatureFilters = ignoreMissingFeatureFilters;
            });

            Console.WriteLine($"[CodeZero] Adds Feature Management to AppServiceLoader...");
        }

        return builder;
    }
}