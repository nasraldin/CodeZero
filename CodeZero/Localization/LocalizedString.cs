//  <copyright file="LocalizedString.cs" project="CodeZero" solution="CodeZero">
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
    /// Represents a localized string.
    /// </summary>
    public class LocalizedString
    {
        /// <summary>
        /// Culture info for this string.
        /// </summary>
        public CultureInfo CultureInfo { get; internal set; }

        /// <summary>
        /// Unique Name of the string.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value for the <see cref="Name"/>.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates a localized string instance.
        /// </summary>
        /// <param name="cultureInfo">Culture info for this string</param>
        /// <param name="name">Unique Name of the string</param>
        /// <param name="value">Value for the <paramref name="name"/></param>
        public LocalizedString(string name, string value, CultureInfo cultureInfo)
        {
            Name = name;
            Value = value;
            CultureInfo = cultureInfo;
        }
    }
}