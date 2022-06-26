using System.Net.NetworkInformation;
using CodeZero.Configuration;
using CodeZero.Utils;
using JetBrains.Annotations;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Register Reverse Proxy.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseProxy(
        [NotNull] this IApplicationBuilder app,
        [NotNull] IConfiguration configuration)
    {
        var proxySettings = configuration.GetSection(nameof(ProxySettings)).Get<ProxySettings>();

        if (!string.IsNullOrEmpty(proxySettings?.RequestBasePath))
        {
            app.Use(async (context, next) =>
            {
                context.Request.PathBase = proxySettings.RequestBasePath;
                await next.Invoke().ConfigureAwait(false);
            });
        }

        var forwardedOptions = new ForwardedHeadersOptions()
        {
            ForwardedHeaders = ForwardedHeaders.All
        };

        if (proxySettings?.ForwardedHeadersOptions is not null &&
            proxySettings.ForwardedHeadersOptions.AddActiveNetworkInterfaceToKnownNetworks)
        {
            foreach (var network in Network.GetNetworks(NetworkInterfaceType.Ethernet))
            {
                forwardedOptions.KnownNetworks.Add(network);
            }
        }

        app.UseForwardedHeaders(forwardedOptions);

        // The default HSTS value is 30 days.
        // You may want to change this for production scenarios,
        // see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();

        return app;
    }
}