using CodeZero.Helpers;
using CodeZero.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
    public static WebApplicationBuilder AddDefaultServices(this WebApplicationBuilder builder)
    {
        Console.WriteLine("[CodeZero] Adds CodeZero services to the host service collection...");

        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddSingleton<IPoweredByMiddlewareOptions, PoweredByMiddlewareOptions>();

        var httpContextAccessor = builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>();
        var httpContextHelper = new HttpContextHelper(httpContextAccessor);
        builder.Services.AddTransient(typeof(IHttpContextHelper), typeof(HttpContextHelper));
        builder.Services.TryAddSingleton<IHttpContextHelper>(HttpContextHelper.Current);
        Console.WriteLine($"[CodeZero] CodeZero Attch HttpContextHelper to runtime: {httpContextHelper.HttpContextAccessor}");

        return builder;
    }
}