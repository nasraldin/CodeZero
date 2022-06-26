using CodeZero;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Register the antiforgery services to be service-aware.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAntiforgeryConfig([NotNull] this IServiceCollection services)
    {
        // Adds host and service level antiforgery services.
        services.AddAntiforgery(options =>
        {
            // Angular's default header name for sending the XSRF token.
            options.HeaderName = AppConsts.HeaderName.Xsrf;
            options.FormFieldName = "__RequestVerificationToken";
            //options.Cookie.Name = "_CodeZeroAntiforgery";

            // Don't set the cookie builder 'Path' so that it uses the 'IAuthenticationFeature' value
            // set by the pipeline and comming from the request 'PathBase' which already ends with the
            // service prefix but may also start by a path related e.g to a virtual folder.
        });

        services.AddMvc(options =>
        {
            // ValidateAntiforgery will validate every request,
            // whereas AutoValidateAntiforgery will only perform validation
            // for unsafe HTTP methods(methods other than GET, HEAD, OPTIONS and TRACE).
            ////options.Filters.Add(new ValidateAntiForgeryTokenAttribute());

            // Forcing AntiForgery Token Validation on by default, it's only in Razor Pages by default
            // Load this filter after the MediaSizeFilterLimitAttribute, but before the
            // IgnoreAntiforgeryTokenAttribute. refer : https://github.com/aspnet/AspNetCore/issues/10384
            options.Filters.Add(typeof(AutoValidateAntiforgeryTokenAttribute), 999);
        });

        return services;
    }
}
