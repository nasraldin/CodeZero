using CodeZero;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Session, Cookies.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration"></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddSessionCookies(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        // Adds backwards compatibility to the handling of SameSite cookies.
        services.AddDistributedMemoryCache();

        var appName = configuration.GetSection("ApplicationName").Value ?? AppDomain.CurrentDomain.FriendlyName;

        services.AddSession(options =>
        {
            options.Cookie.Name = $".{appName}.Session";
            options.IdleTimeout = TimeSpan.FromSeconds(60);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            options.OnAppendCookie = cookieContext =>
            CheckSameSiteBackwardsCompatiblity(cookieContext.Context, cookieContext.CookieOptions);
            options.OnDeleteCookie = cookieContext =>
            CheckSameSiteBackwardsCompatiblity(cookieContext.Context, cookieContext.CookieOptions);
        });

        return services;
    }

    private static void CheckSameSiteBackwardsCompatiblity(HttpContext httpContext, CookieOptions options)
    {
        var userAgent = httpContext.Request.Headers[AppConsts.HeaderName.UserAgent].ToString();

        if (options.SameSite == SameSiteMode.None)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                return;
            }

            // Cover all iOS based browsers here. This includes:
            // - Safari on iOS 12 for iPhone, iPod Touch, iPad
            // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
            // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
            // All of which are broken by SameSite=None, because they use the iOS networking stack
            if (userAgent.Contains("CPU iPhone OS 12") || userAgent.Contains("iPad; CPU OS 12"))
            {
                options.SameSite = SameSiteMode.Unspecified;
                return;
            }

            // Cover Mac OS X based browsers that use the Mac OS networking stack. This includes:
            // - Safari on Mac OS X.
            // This does not include:
            // - Chrome on Mac OS X
            // Because they do not use the Mac OS networking stack.
            if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                userAgent.Contains("Version/") && userAgent.Contains("Safari"))
            {
                options.SameSite = SameSiteMode.Unspecified;
                return;
            }

            // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
            // and none in this range require it.
            // Note: this covers some pre-Chromium Edge versions, 
            // but pre-Chromium Edge does not require SameSite=None.
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
            {
                options.SameSite = SameSiteMode.Unspecified;
            }
        }
    }
}