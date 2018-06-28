//  <copyright file="ILanguageManagementConfig.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Localization.Dictionaries;

namespace CodeZero.Identity.Configuration
{
    /// <summary>
    /// Used to configure language management.
    /// </summary>
    public interface ILanguageManagementConfig
    {
        /// <summary>
        /// Enables the database localization.
        /// Replaces all <see cref="IDictionaryBasedLocalizationSource"/> localization sources
        /// with database based localization source.
        /// </summary>
        void EnableDbLocalization();
    }
}