//  <copyright file="ILocalizationSource.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Globalization;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;

namespace CodeZero.Localization.Sources
{
    /// <summary>
    /// A Localization Source is used to obtain localized strings.
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        /// Unique Name of the source.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// This method is called by CodeZero before first usage.
        /// </summary>
        void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver);

        /// <summary>
        /// Gets localized string for given name in current language.
        /// Fallbacks to default language if not found in current culture.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        string GetString(string name);

        /// <summary>
        /// Gets localized string for given name and specified culture.
        /// Fallbacks to default language if not found in given culture.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        string GetString(string name, CultureInfo culture);

        /// <summary>
        /// Gets localized string for given name in current language.
        /// Returns null if not found.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// </param>
        /// <returns>Localized string</returns>
        string GetStringOrNull(string name, bool tryDefaults = true);

        /// <summary>
        /// Gets localized string for given name and specified culture.
        /// Returns null if not found.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// </param>
        /// <returns>Localized string</returns>
        string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true);

        /// <summary>
        /// Gets all strings in current language.
        /// </summary>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true);

        /// <summary>
        /// Gets all strings in specified culture.
        /// </summary>
        /// <param name="culture">culture information</param>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true);
    }
}