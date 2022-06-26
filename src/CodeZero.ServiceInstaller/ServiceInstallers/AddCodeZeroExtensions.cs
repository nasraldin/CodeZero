namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Adds CodeZero services to the host service collection
/// Registration of the dependency in a service container.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds CodeZero services to the host service collection and let the app change
    /// the default behavior and set of features through a configure action.
    /// </summary>
    public static WebApplicationBuilder AddCodeZero(this WebApplicationBuilder builder)
    {
        builder.AddAppServiceLoader();
        builder.AddDefaultServices();

        return builder;
    }
}