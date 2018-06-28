//  <copyright file="CurrentCultureSetter.cs" project="CodeZero.Web" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using CodeZero.Collections.Extensions;
using CodeZero.Configuration;
using CodeZero.Dependency;
using CodeZero.Extensions;
using CodeZero.Localization;
using CodeZero.Runtime.Session;
using CodeZero.Timing;
using CodeZero.Web.Configuration;

namespace CodeZero.Web.Localization
{
    public class CurrentCultureSetter : ICurrentCultureSetter, ITransientDependency
    {
        private readonly ICodeZeroWebLocalizationConfiguration _webLocalizationConfiguration;
        private readonly ISettingManager _settingManager;
        private readonly ICodeZeroSession _CodeZeroSession;

        public CurrentCultureSetter(
            ICodeZeroWebLocalizationConfiguration webLocalizationConfiguration,
            ISettingManager settingManager,
            ICodeZeroSession CodeZeroSession)
        {
            _webLocalizationConfiguration = webLocalizationConfiguration;
            _settingManager = settingManager;
            _CodeZeroSession = CodeZeroSession;
        }

        public virtual void SetCurrentCulture(HttpContext httpContext)
        {
            if (IsCultureSpecifiedInGlobalizationConfig())
            {
                return;
            }

            // 1: Query String
            var culture = GetCultureFromQueryString(httpContext);
            if (culture != null)
            {
                SetCurrentCulture(culture);
                return;
            }

            // 2: User preference
            culture = GetCultureFromUserSetting();
            if (culture != null)
            {
                SetCurrentCulture(culture);
                return;
            }

            // 3 & 4: Header / Cookie
            culture = GetCultureFromHeader(httpContext) ?? GetCultureFromCookie(httpContext);
            if (culture != null)
            {
                if (_CodeZeroSession.UserId.HasValue)
                {
                    SetCultureToUserSetting(_CodeZeroSession.ToUserIdentifier(), culture);
                }

                SetCurrentCulture(culture);
                return;
            }

            // 5 & 6: Default / Browser
            culture = GetDefaultCulture() ?? GetBrowserCulture(httpContext);
            if (culture != null)
            {
                SetCurrentCulture(culture);
                SetCultureToCookie(httpContext, culture);
            }
        }

        private void SetCultureToUserSetting(UserIdentifier user, string culture)
        {
            _settingManager.ChangeSettingForUser(
                user,
                LocalizationSettingNames.DefaultLanguage,
                culture
            );
        }

        private string GetCultureFromUserSetting()
        {
            if (_CodeZeroSession.UserId == null)
            {
                return null;
            }

            var culture = _settingManager.GetSettingValueForUser(
                LocalizationSettingNames.DefaultLanguage,
                _CodeZeroSession.TenantId,
                _CodeZeroSession.UserId.Value,
                fallbackToDefault: false
            );

            if (culture.IsNullOrEmpty() || !GlobalizationHelper.IsValidCultureCode(culture))
            {
                return null;
            }

            return culture;
        }

        protected virtual bool IsCultureSpecifiedInGlobalizationConfig()
        {
            var globalizationSection = WebConfigurationManager.GetSection("system.web/globalization") as GlobalizationSection;
            if (globalizationSection == null || globalizationSection.UICulture.IsNullOrEmpty())
            {
                return false;
            }

            return !string.Equals(globalizationSection.UICulture, "auto", StringComparison.InvariantCultureIgnoreCase);
        }

        protected virtual string GetCultureFromCookie(HttpContext httpContext)
        {
            var culture = httpContext.Request.Cookies[_webLocalizationConfiguration.CookieName]?.Value;
            if (culture.IsNullOrEmpty() || !GlobalizationHelper.IsValidCultureCode(culture))
            {
                return null;
            }

            return culture;
        }

        protected virtual void SetCultureToCookie(HttpContext context, string culture)
        {
            context.Response.SetCookie(
                new HttpCookie(_webLocalizationConfiguration.CookieName, culture)
                {
                    Expires = Clock.Now.AddYears(2),
                    Path = context.Request.ApplicationPath
                }
            );
        }

        protected virtual string GetDefaultCulture()
        {
            var culture = _settingManager.GetSettingValue(LocalizationSettingNames.DefaultLanguage);
            if (culture.IsNullOrEmpty() || !GlobalizationHelper.IsValidCultureCode(culture))
            {
                return null;
            }

            return culture;
        }

        protected virtual string GetCultureFromHeader(HttpContext httpContext)
        {
            var culture = httpContext.Request.Headers[_webLocalizationConfiguration.CookieName];
            if (culture.IsNullOrEmpty() || !GlobalizationHelper.IsValidCultureCode(culture))
            {
                return null;
            }

            return culture;
        }

        protected virtual string GetBrowserCulture(HttpContext httpContext)
        {
            if (httpContext.Request.UserLanguages.IsNullOrEmpty())
            {
                return null;
            }

            return httpContext.Request?.UserLanguages?.FirstOrDefault(GlobalizationHelper.IsValidCultureCode);
        }

        protected virtual string GetCultureFromQueryString(HttpContext httpContext)
        {
            var culture = httpContext.Request.QueryString[_webLocalizationConfiguration.CookieName];
            if (culture.IsNullOrEmpty() || !GlobalizationHelper.IsValidCultureCode(culture))
            {
                return null;
            }

            return culture;
        }

        protected virtual void SetCurrentCulture(string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfoHelper.Get(language);
            Thread.CurrentThread.CurrentUICulture = CultureInfoHelper.Get(language);
        }
    }
}