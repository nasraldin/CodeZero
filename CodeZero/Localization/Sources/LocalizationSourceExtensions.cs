//  <copyright file="LocalizationSourceExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Globalization;

namespace CodeZero.Localization.Sources
{
    /// <summary>
    /// Extension methods for <see cref="ILocalizationSource"/>.
    /// </summary>
    public static class LocalizationSourceExtensions
    {
        /// <summary>
        /// Get a localized string by formatting string.
        /// </summary>
        /// <param name="source">Localization source</param>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Formatted and localized string</returns>
        public static string GetString(this ILocalizationSource source, string name, params object[] args)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return string.Format(source.GetString(name), args);
        }

        /// <summary>
        /// Get a localized string in given language by formatting string.
        /// </summary>
        /// <param name="source">Localization source</param>
        /// <param name="name">Key name</param>
        /// <param name="culture">Culture</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Formatted and localized string</returns>
        public static string GetString(this ILocalizationSource source, string name, CultureInfo culture, params object[] args)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return string.Format(source.GetString(name, culture), args);
        }
    }
}