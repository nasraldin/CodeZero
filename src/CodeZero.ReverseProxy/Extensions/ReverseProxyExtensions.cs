using System.Net;
using System.Net.NetworkInformation;
using CodeZero.Configuration;
using CodeZero.Utils;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Reverse Proxy.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddProxy(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        var proxySettings = configuration.GetSection(nameof(ProxySettings)).Get<ProxySettings>();

        if (proxySettings?.ForwardedHeadersOptions is not null)
        {
            // Configure ASP.NET Core to work with proxy servers and load balancers
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

                if (proxySettings.ForwardedHeadersOptions.ForwardLimit.HasValue)
                    options.ForwardLimit = proxySettings.ForwardedHeadersOptions.ForwardLimit;

                if (!string.IsNullOrEmpty(proxySettings.ForwardedHeadersOptions.ForwardedForHeaderName))
                    options.ForwardedForHeaderName = proxySettings.ForwardedHeadersOptions.ForwardedForHeaderName;

                if (!string.IsNullOrEmpty(proxySettings.ForwardedHeadersOptions.ForwardedHostHeaderName))
                    options.ForwardedHostHeaderName = proxySettings.ForwardedHeadersOptions.ForwardedHostHeaderName;

                if (!string.IsNullOrEmpty(proxySettings.ForwardedHeadersOptions.ForwardedProtoHeaderName))
                    options.ForwardedProtoHeaderName = proxySettings.ForwardedHeadersOptions.ForwardedProtoHeaderName;

                if (!string.IsNullOrEmpty(proxySettings.ForwardedHeadersOptions.OriginalForHeaderName))
                    options.OriginalForHeaderName = proxySettings.ForwardedHeadersOptions.OriginalForHeaderName;

                if (!string.IsNullOrEmpty(proxySettings.ForwardedHeadersOptions.OriginalHostHeaderName))
                    options.OriginalHostHeaderName = proxySettings.ForwardedHeadersOptions.OriginalHostHeaderName;

                if (!string.IsNullOrEmpty(proxySettings.ForwardedHeadersOptions.OriginalProtoHeaderName))
                    options.OriginalProtoHeaderName = proxySettings.ForwardedHeadersOptions.OriginalProtoHeaderName;

                // Only loopback proxies are allowed by default.
                // Clear that restriction because forwarders are enabled by explicit configuration.
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();

                if (proxySettings.ForwardedHeadersOptions.KnownProxies.Any())
                {
                    proxySettings.ForwardedHeadersOptions.KnownProxies.ForEach(item =>
                    {
                        options.KnownProxies.Add(IPAddress.Parse(item));
                    });
                }

                if (proxySettings.ForwardedHeadersOptions.AddActiveNetworkInterfaceToKnownNetworks)
                {
                    foreach (var network in Network.GetNetworks(NetworkInterfaceType.Ethernet))
                    {
                        options.KnownNetworks.Add(network);
                    }
                }

                if (proxySettings.ForwardedHeadersOptions.RequireHeaderSymmetry)
                    options.RequireHeaderSymmetry = proxySettings.ForwardedHeadersOptions.RequireHeaderSymmetry;
            });
        }

        if (proxySettings?.HstsOptions is not null)
        {
            // Enforce HTTPS
            services.AddHsts(options =>
            {
                if (proxySettings.HstsOptions.Preload)
                    options.Preload = proxySettings.HstsOptions.Preload;

                if (proxySettings.HstsOptions.IncludeSubDomains)
                    options.IncludeSubDomains = proxySettings.HstsOptions.IncludeSubDomains;

                if (!proxySettings.HstsOptions.MaxAge.Equals(0))
                    options.MaxAge = TimeSpan.FromDays(proxySettings.HstsOptions.MaxAge);

                if (proxySettings.HstsOptions.ExcludedHosts.Any())
                {
                    proxySettings.HstsOptions.ExcludedHosts.ForEach(item =>
                    {
                        options.ExcludedHosts.Add(item);
                    });
                }
            });
        }

        if (proxySettings?.HttpsRedirectionOptions is not null)
        {
            // Configure temporary/permanent redirects in production (Status308PermanentRedirect)
            services.AddHttpsRedirection(options =>
            {
                if (!proxySettings.HttpsRedirectionOptions.RedirectStatusCode.Equals(0))
                    options.RedirectStatusCode = proxySettings.HttpsRedirectionOptions.RedirectStatusCode;

                if (proxySettings.HttpsRedirectionOptions.HttpsPort.HasValue)
                    options.HttpsPort = proxySettings.HttpsRedirectionOptions.HttpsPort.Value;
            });
        }

        return services;
    }


    /// <summary>
    /// Enumerate each element in the enumeration and execute specified action
    /// </summary>
    /// <typeparam name="T">Type of enumeration</typeparam>
    /// <param name="enumerable">Enumerable collection</param>
    /// <param name="action">Action to perform</param>
    private static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (T item in enumerable)
            action(item);
    }
}
