//  <copyright file="IDictionaryBasedLocalizationSource.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Localization.Sources;

namespace CodeZero.Localization.Dictionaries
{
    /// <summary>
    /// Interface for a dictionary based localization source.
    /// </summary>
    public interface IDictionaryBasedLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// Gets the dictionary provider.
        /// </summary>
        ILocalizationDictionaryProvider DictionaryProvider { get; }

        /// <summary>
        /// Extends the source with given dictionary.
        /// </summary>
        /// <param name="dictionary">Dictionary to extend the source</param>
        void Extend(ILocalizationDictionary dictionary);
    }
}