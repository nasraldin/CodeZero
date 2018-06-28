//  <copyright file="LocalizationHelper.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Globalization;
using CodeZero.Dependency;
using CodeZero.Localization.Sources;

namespace CodeZero.Localization
{
    /// <summary>
    /// This static class is used to simplify getting localized strings.
    /// </summary>
    public static class LocalizationHelper
    {
        /// <summary>
        /// Gets a reference to the localization manager.
        /// Inject and use <see cref="ILocalizationManager"/>
        /// wherever it's possible, instead of this shortcut.
        /// </summary>
        public static ILocalizationManager Manager { get { return LocalizationManager.Value; } }

        private static readonly Lazy<ILocalizationManager> LocalizationManager;

        static LocalizationHelper()
        {
            LocalizationManager = new Lazy<ILocalizationManager>(
                () => IocManager.Instance.IsRegistered<ILocalizationManager>()
                    ? IocManager.Instance.Resolve<ILocalizationManager>()
                    : NullLocalizationManager.Instance
                );
        }

        /// <summary>
        /// Gets a pre-registered localization source.
        /// </summary>
        public static ILocalizationSource GetSource(string name)
        {
            return LocalizationManager.Value.GetSource(name);
        }

        /// <summary>
        /// Gets a localized string in current language.
        /// </summary>
        /// <param name="sourceName">Name of the localization source</param>
        /// <param name="name">Key name to get localized string</param>
        /// <returns>Localized string</returns>
        public static string GetString(string sourceName, string name)
        {
            return LocalizationManager.Value.GetString(sourceName, name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// </summary>
        /// <param name="sourceName">Name of the localization source</param>
        /// <param name="name">Key name to get localized string</param>
        /// <param name="culture">culture</param>
        /// <returns>Localized string</returns>
        public static string GetString(string sourceName, string name, CultureInfo culture)
        {
            return LocalizationManager.Value.GetString(sourceName, name, culture);
        }
    }
}