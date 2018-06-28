//  <copyright file="ILocalizableString.cs" project="CodeZero" solution="CodeZero">
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
    /// <summary>
    /// Represents a string that can be localized when needed.
    /// </summary>
    public interface ILocalizableString
    {
        /// <summary>
        /// Localizes the string in current culture.
        /// </summary>
        /// <param name="context">Localization context</param>
        /// <returns>Localized string</returns>
        string Localize(ILocalizationContext context);

        /// <summary>
        /// Localizes the string in given culture.
        /// </summary>
        /// <param name="context">Localization context</param>
        /// <param name="culture">culture</param>
        /// <returns>Localized string</returns>
        string Localize(ILocalizationContext context, CultureInfo culture);
    }
}