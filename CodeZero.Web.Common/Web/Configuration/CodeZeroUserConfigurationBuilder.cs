//  <copyright file="CodeZeroUserConfigurationBuilder.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Application.Features;
using CodeZero.Application.Navigation;
using CodeZero.Authorization;
using CodeZero.Configuration;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Extensions;
using CodeZero.Localization;
using CodeZero.Runtime.Session;
using CodeZero.Timing;
using CodeZero.Timing.Timezone;
using CodeZero.Web.Models.CodeZeroUserConfiguration;
using CodeZero.Web.Security.AntiForgery;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CodeZero.Web.Configuration
{
    public class CodeZeroUserConfigurationBuilder : ITransientDependency
    {
        private readonly ICodeZeroStartupConfiguration _startupConfiguration;

        protected IMultiTenancyConfig MultiTenancyConfig { get; }
        protected ILanguageManager LanguageManager { get; }
        protected ILocalizationManager LocalizationManager { get; }
        protected IFeatureManager FeatureManager { get; }
        protected IFeatureChecker FeatureChecker { get; }
        protected IPermissionManager PermissionManager { get; }
        protected IUserNavigationManager UserNavigationManager { get; }
        protected ISettingDefinitionManager SettingDefinitionManager { get; }
        protected ISettingManager SettingManager { get; }
        protected ICodeZeroAntiForgeryConfiguration CodeZeroAntiForgeryConfiguration { get; }
        protected ICodeZeroSession CodeZeroSession { get; }
        protected IPermissionChecker PermissionChecker { get; }
        protected Dictionary<string, object> CustomDataConfig { get; }

        private readonly IIocResolver _iocResolver;

        public CodeZeroUserConfigurationBuilder(
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager,
            ILocalizationManager localizationManager,
            IFeatureManager featureManager,
            IFeatureChecker featureChecker,
            IPermissionManager permissionManager,
            IUserNavigationManager userNavigationManager,
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            ICodeZeroAntiForgeryConfiguration codeZeroAntiForgeryConfiguration,
            ICodeZeroSession codeZeroSession,
            IPermissionChecker permissionChecker,
            IIocResolver iocResolver,
            ICodeZeroStartupConfiguration startupConfiguration)
        {
            MultiTenancyConfig = multiTenancyConfig;
            LanguageManager = languageManager;
            LocalizationManager = localizationManager;
            FeatureManager = featureManager;
            FeatureChecker = featureChecker;
            PermissionManager = permissionManager;
            UserNavigationManager = userNavigationManager;
            SettingDefinitionManager = settingDefinitionManager;
            SettingManager = settingManager;
            CodeZeroAntiForgeryConfiguration = codeZeroAntiForgeryConfiguration;
            CodeZeroSession = codeZeroSession;
            PermissionChecker = permissionChecker;
            _iocResolver = iocResolver;
            _startupConfiguration = startupConfiguration;

            CustomDataConfig = new Dictionary<string, object>();
        }

        public virtual async Task<CodeZeroUserConfigurationDto> GetAll()
        {
            return new CodeZeroUserConfigurationDto
            {
                MultiTenancy = GetUserMultiTenancyConfig(),
                Session = GetUserSessionConfig(),
                Localization = GetUserLocalizationConfig(),
                Features = await GetUserFeaturesConfig(),
                Auth = await GetUserAuthConfig(),
                Nav = await GetUserNavConfig(),
                Setting = await GetUserSettingConfig(),
                Clock = GetUserClockConfig(),
                Timing = await GetUserTimingConfig(),
                Security = GetUserSecurityConfig(),
                Custom = _startupConfiguration.GetCustomConfig()
            };
        }

        protected virtual CodeZeroMultiTenancyConfigDto GetUserMultiTenancyConfig()
        {
            return new CodeZeroMultiTenancyConfigDto
            {
                IsEnabled = MultiTenancyConfig.IsEnabled
            };
        }

        protected virtual CodeZeroUserSessionConfigDto GetUserSessionConfig()
        {
            return new CodeZeroUserSessionConfigDto
            {
                UserId = CodeZeroSession.UserId,
                TenantId = CodeZeroSession.TenantId,
                ImpersonatorUserId = CodeZeroSession.ImpersonatorUserId,
                ImpersonatorTenantId = CodeZeroSession.ImpersonatorTenantId,
                MultiTenancySide = CodeZeroSession.MultiTenancySide
            };
        }

        protected virtual CodeZeroUserLocalizationConfigDto GetUserLocalizationConfig()
        {
            var currentCulture = CultureInfo.CurrentUICulture;
            var languages = LanguageManager.GetLanguages();

            var config = new CodeZeroUserLocalizationConfigDto
            {
                CurrentCulture = new CodeZeroUserCurrentCultureConfigDto
                {
                    Name = currentCulture.Name,
                    DisplayName = currentCulture.DisplayName
                },
                Languages = languages.ToList()
            };

            if (languages.Count > 0)
            {
                config.CurrentLanguage = LanguageManager.CurrentLanguage;
            }

            var sources = LocalizationManager.GetAllSources().OrderBy(s => s.Name).ToArray();
            config.Sources = sources.Select(s => new CodeZeroLocalizationSourceDto
            {
                Name = s.Name,
                Type = s.GetType().Name
            }).ToList();

            config.Values = new Dictionary<string, Dictionary<string, string>>();
            foreach (var source in sources)
            {
                var stringValues = source.GetAllStrings(currentCulture).OrderBy(s => s.Name).ToList();
                var stringDictionary = stringValues
                    .ToDictionary(_ => _.Name, _ => _.Value);
                config.Values.Add(source.Name, stringDictionary);
            }

            return config;
        }

        protected virtual async Task<CodeZeroUserFeatureConfigDto> GetUserFeaturesConfig()
        {
            var config = new CodeZeroUserFeatureConfigDto()
            {
                AllFeatures = new Dictionary<string, CodeZeroStringValueDto>()
            };

            var allFeatures = FeatureManager.GetAll().ToList();

            if (CodeZeroSession.TenantId.HasValue)
            {
                var currentTenantId = CodeZeroSession.GetTenantId();
                foreach (var feature in allFeatures)
                {
                    var value = await FeatureChecker.GetValueAsync(currentTenantId, feature.Name);
                    config.AllFeatures.Add(feature.Name, new CodeZeroStringValueDto
                    {
                        Value = value
                    });
                }
            }
            else
            {
                foreach (var feature in allFeatures)
                {
                    config.AllFeatures.Add(feature.Name, new CodeZeroStringValueDto
                    {
                        Value = feature.DefaultValue
                    });
                }
            }

            return config;
        }

        protected virtual async Task<CodeZeroUserAuthConfigDto> GetUserAuthConfig()
        {
            var config = new CodeZeroUserAuthConfigDto();

            var allPermissionNames = PermissionManager.GetAllPermissions(false).Select(p => p.Name).ToList();
            var grantedPermissionNames = new List<string>();

            if (CodeZeroSession.UserId.HasValue)
            {
                foreach (var permissionName in allPermissionNames)
                {
                    if (await PermissionChecker.IsGrantedAsync(permissionName))
                    {
                        grantedPermissionNames.Add(permissionName);
                    }
                }
            }

            config.AllPermissions = allPermissionNames.ToDictionary(permissionName => permissionName, permissionName => "true");
            config.GrantedPermissions = grantedPermissionNames.ToDictionary(permissionName => permissionName, permissionName => "true");

            return config;
        }

        protected virtual async Task<CodeZeroUserNavConfigDto> GetUserNavConfig()
        {
            var userMenus = await UserNavigationManager.GetMenusAsync(CodeZeroSession.ToUserIdentifier());
            return new CodeZeroUserNavConfigDto
            {
                Menus = userMenus.ToDictionary(userMenu => userMenu.Name, userMenu => userMenu)
            };
        }

        protected virtual async Task<CodeZeroUserSettingConfigDto> GetUserSettingConfig()
        {
            var config = new CodeZeroUserSettingConfigDto
            {
                Values = new Dictionary<string, string>()
            };

            var settingDefinitions = SettingDefinitionManager
                .GetAllSettingDefinitions();

            using (var scope = _iocResolver.CreateScope())
            {
                foreach (var settingDefinition in settingDefinitions)
                {
                    if (!await settingDefinition.ClientVisibilityProvider.CheckVisible(scope))
                    {
                        continue;
                    }

                    var settingValue = await SettingManager.GetSettingValueAsync(settingDefinition.Name);
                    config.Values.Add(settingDefinition.Name, settingValue);
                }
            }

            return config;
        }

        protected virtual CodeZeroUserClockConfigDto GetUserClockConfig()
        {
            return new CodeZeroUserClockConfigDto
            {
                Provider = Clock.Provider.GetType().Name.ToCamelCase()
            };
        }

        protected virtual async Task<CodeZeroUserTimingConfigDto> GetUserTimingConfig()
        {
            var timezoneId = await SettingManager.GetSettingValueAsync(TimingSettingNames.TimeZone);
            var timezone = TimezoneHelper.FindTimeZoneInfo(timezoneId);

            return new CodeZeroUserTimingConfigDto
            {
                TimeZoneInfo = new CodeZeroUserTimeZoneConfigDto
                {
                    Windows = new CodeZeroUserWindowsTimeZoneConfigDto
                    {
                        TimeZoneId = timezoneId,
                        BaseUtcOffsetInMilliseconds = timezone.BaseUtcOffset.TotalMilliseconds,
                        CurrentUtcOffsetInMilliseconds = timezone.GetUtcOffset(Clock.Now).TotalMilliseconds,
                        IsDaylightSavingTimeNow = timezone.IsDaylightSavingTime(Clock.Now)
                    },
                    Iana = new CodeZeroUserIanaTimeZoneConfigDto
                    {
                        TimeZoneId = TimezoneHelper.WindowsToIana(timezoneId)
                    }
                }
            };
        }

        protected virtual CodeZeroUserSecurityConfigDto GetUserSecurityConfig()
        {
            return new CodeZeroUserSecurityConfigDto
            {
                AntiForgery = new CodeZeroUserAntiForgeryConfigDto
                {
                    TokenCookieName = CodeZeroAntiForgeryConfiguration.TokenCookieName,
                    TokenHeaderName = CodeZeroAntiForgeryConfiguration.TokenHeaderName
                }
            };
        }
    }
}
