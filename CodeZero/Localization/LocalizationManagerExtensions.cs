//  <copyright file="LocalizationManagerExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Globalization;

namespace CodeZero.Localization
{
    public static class LocalizationManagerExtensions
    {
        /// <summary>
        /// Gets a localized string in current language.
        /// </summary>
        /// <returns>Localized string</returns>
        public static string GetString(this ILocalizationManager localizationManager, LocalizableString localizableString)
        {
            return localizationManager.GetString(localizableString.SourceName, localizableString.Name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// </summary>
        /// <returns>Localized string</returns>
        public static string GetString(this ILocalizationManager localizationManager, LocalizableString localizableString, CultureInfo culture)
        {
            return localizationManager.GetString(localizableString.SourceName, localizableString.Name, culture);
        }

        /// <summary>
        /// Gets a localized string in current language.
        /// </summary>
        /// <param name="localizationManager">Localization manager instance</param>
        /// <param name="sourceName">Name of the localization source</param>
        /// <param name="name">Key name to get localized string</param>
        /// <returns>Localized string</returns>
        public static string GetString(this ILocalizationManager localizationManager, string sourceName, string name)
        {
            return localizationManager.GetSource(sourceName).GetString(name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// </summary>
        /// <param name="localizationManager">Localization manager instance</param>
        /// <param name="sourceName">Name of the localization source</param>
        /// <param name="name">Key name to get localized string</param>
        /// <param name="culture">culture</param>
        /// <returns>Localized string</returns>
        public static string GetString(this ILocalizationManager localizationManager, string sourceName, string name, CultureInfo culture)
        {
            return localizationManager.GetSource(sourceName).GetString(name, culture);
        }
    }
}