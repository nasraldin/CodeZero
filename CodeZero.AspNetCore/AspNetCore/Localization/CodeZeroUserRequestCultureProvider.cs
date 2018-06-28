//  <copyright file="CodeZeroUserRequestCultureProvider.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using System.Threading.Tasks;
using CodeZero.Configuration;
using CodeZero.Runtime.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using CodeZero.Localization;
using CodeZero.Extensions;
using JetBrains.Annotations;

namespace CodeZero.AspNetCore.Localization
{
    public class CodeZeroUserRequestCultureProvider : RequestCultureProvider
    {
        public CookieRequestCultureProvider CookieProvider { get; set; }
        public CodeZeroLocalizationHeaderRequestCultureProvider HeaderProvider { get; set; }

        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var CodeZeroSession = httpContext.RequestServices.GetRequiredService<ICodeZeroSession>();
            if (CodeZeroSession.UserId == null)
            {
                return null;
            }

            var settingManager = httpContext.RequestServices.GetRequiredService<ISettingManager>();

            var culture = await settingManager.GetSettingValueForUserAsync(
                LocalizationSettingNames.DefaultLanguage,
                CodeZeroSession.TenantId,
                CodeZeroSession.UserId.Value,
                fallbackToDefault: false
            );

            if (!culture.IsNullOrEmpty())
            {
                return new ProviderCultureResult(culture, culture);
            }

            var result = await GetResultOrNull(httpContext, CookieProvider) ??
                         await GetResultOrNull(httpContext, HeaderProvider);

            if (result == null || !result.Cultures.Any())
            {
                return null;
            }

            //Try to set user's language setting from cookie if available.
            await settingManager.ChangeSettingForUserAsync(
                CodeZeroSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                result.Cultures.First().Value
            );

            return result;
        }

        protected virtual async Task<ProviderCultureResult> GetResultOrNull([NotNull] HttpContext httpContext, [CanBeNull] IRequestCultureProvider provider)
        {
            if (provider == null)
            {
                return null;
            }

            return await provider.DetermineProviderCultureResult(httpContext);
        }
    }
}
