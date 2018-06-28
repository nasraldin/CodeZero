//  <copyright file="JsonLocalizationFile.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;

namespace CodeZero.Localization.Dictionaries.Json
{
    /// <summary>
    /// Use it to serialize json file
    /// </summary>
    public class JsonLocalizationFile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JsonLocalizationFile()
        {
            Texts = new Dictionary<string, string>();
        }

        /// <summary>
        /// get or set the culture name; eg : en , en-us, zh-CN
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        ///  Key value pairs
        /// </summary>
        public Dictionary<string, string> Texts { get; private set; }
    }
}