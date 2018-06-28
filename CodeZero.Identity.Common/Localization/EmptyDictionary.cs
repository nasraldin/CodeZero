//  <copyright file="EmptyDictionary.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Globalization;
using CodeZero.Localization.Dictionaries;

namespace CodeZero.Localization
{
    internal class EmptyDictionary : ILocalizationDictionary
    {
        public CultureInfo CultureInfo { get; private set; }

        public EmptyDictionary(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
        }

        public LocalizedString GetOrNull(string name)
        {
            return null;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings()
        {
            return new LocalizedString[0];
        }

        public string this[string name]
        {
            get { return null; }
            set { }
        }
    }
}