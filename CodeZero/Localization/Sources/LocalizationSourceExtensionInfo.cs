//  <copyright file="LocalizationSourceExtensionInfo.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Localization.Dictionaries;

namespace CodeZero.Localization.Sources
{
    /// <summary>
    /// Used to store a localization source extension information.
    /// </summary>
    public class LocalizationSourceExtensionInfo
    {
        /// <summary>
        /// Source name.
        /// </summary>
        public string SourceName { get; private set; }

        /// <summary>
        /// Extension dictionaries.
        /// </summary>
        public ILocalizationDictionaryProvider DictionaryProvider { get; private set; }

        /// <summary>
        /// Creates a new <see cref="LocalizationSourceExtensionInfo"/> object.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="dictionaryProvider">Extension dictionaries</param>
        public LocalizationSourceExtensionInfo(string sourceName, ILocalizationDictionaryProvider dictionaryProvider)
        {
            SourceName = sourceName;
            DictionaryProvider = dictionaryProvider;
        }
    }
}