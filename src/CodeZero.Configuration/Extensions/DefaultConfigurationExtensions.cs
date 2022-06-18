using CodeZero.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Adds CodeZero Configuration services to the host service collection
/// </summary>
public static partial class DefaultConfigurationExtensions
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

        return builder;
    }
}