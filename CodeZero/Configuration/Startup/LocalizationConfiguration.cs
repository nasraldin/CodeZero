//  <copyright file="LocalizationConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.Localization;

namespace CodeZero.Configuration.Startup
{
    /// <summary>
    /// Used for localization configurations.
    /// </summary>
    internal class LocalizationConfiguration : ILocalizationConfiguration
    {
        /// <inheritdoc/>
        public IList<LanguageInfo> Languages { get; }

        /// <inheritdoc/>
        public ILocalizationSourceList Sources { get; }

        /// <inheritdoc/>
        public bool IsEnabled { get; set; }

        /// <inheritdoc/>
        public bool ReturnGivenTextIfNotFound { get; set; }

        /// <inheritdoc/>
        public bool WrapGivenTextIfNotFound { get; set; }

        /// <inheritdoc/>
        public bool HumanizeTextIfNotFound { get; set; }

        public bool LogWarnMessageIfNotFound { get; set; }

        public LocalizationConfiguration()
        {
            Languages = new List<LanguageInfo>();
            Sources = new LocalizationSourceList();

            IsEnabled = true;
            ReturnGivenTextIfNotFound = true;
            WrapGivenTextIfNotFound = true;
            HumanizeTextIfNotFound = true;
            LogWarnMessageIfNotFound = true;
        }
    }
}
