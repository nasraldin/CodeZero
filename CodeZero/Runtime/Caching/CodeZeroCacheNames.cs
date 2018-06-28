//  <copyright file="CodeZeroCacheNames.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Runtime.Caching
{
    /// <summary>
    /// Names of standard caches used in CodeZero.
    /// </summary>
    public static class CodeZeroCacheNames
    {
        /// <summary>
        /// Application settings cache: CodeZeroApplicationSettingsCache.
        /// </summary>
        public const string ApplicationSettings = "CodeZeroApplicationSettingsCache";

        /// <summary>
        /// Tenant settings cache: CodeZeroTenantSettingsCache.
        /// </summary>
        public const string TenantSettings = "CodeZeroTenantSettingsCache";

        /// <summary>
        /// User settings cache: CodeZeroUserSettingsCache.
        /// </summary>
        public const string UserSettings = "CodeZeroUserSettingsCache";

        /// <summary>
        /// Localization scripts cache: CodeZeroLocalizationScripts.
        /// </summary>
        public const string LocalizationScripts = "CodeZeroLocalizationScripts";
    }
}