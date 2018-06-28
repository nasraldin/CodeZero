//  <copyright file="JsonLocalizationDictionary.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CodeZero.Collections.Extensions;
using CodeZero.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CodeZero.Localization.Dictionaries.Json
{
    /// <summary>
    ///     This class is used to build a localization dictionary from json.
    /// </summary>
    /// <remarks>
    ///     Use static Build methods to create instance of this class.
    /// </remarks>
    public class JsonLocalizationDictionary : LocalizationDictionary
    {
        /// <summary>
        ///     Private constructor.
        /// </summary>
        /// <param name="cultureInfo">Culture of the dictionary</param>
        private JsonLocalizationDictionary(CultureInfo cultureInfo)
            : base(cultureInfo)
        {
        }

        /// <summary>
        ///     Builds an <see cref="JsonLocalizationDictionary" /> from given file.
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        public static JsonLocalizationDictionary BuildFromFile(string filePath)
        {
            try
            {
                return BuildFromJsonString(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                throw new CodeZeroException("Invalid localization file format! " + filePath, ex);
            }
        }

        /// <summary>
        ///     Builds an <see cref="JsonLocalizationDictionary" /> from given json string.
        /// </summary>
        /// <param name="jsonString">Json string</param>
        public static JsonLocalizationDictionary BuildFromJsonString(string jsonString)
        {
            JsonLocalizationFile jsonFile;
            try
            {
                jsonFile = JsonConvert.DeserializeObject<JsonLocalizationFile>(
                    jsonString,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            }
            catch (JsonException ex)
            {
                throw new CodeZeroException("Can not parse json string. " + ex.Message);
            }

            var cultureCode = jsonFile.Culture;
            if (string.IsNullOrEmpty(cultureCode))
            {
                throw new CodeZeroException("Culture is empty in language json file.");
            }

            var dictionary = new JsonLocalizationDictionary(CultureInfo.GetCultureInfo(cultureCode));
            var dublicateNames = new List<string>();
            foreach (var item in jsonFile.Texts)
            {
                if (string.IsNullOrEmpty(item.Key))
                {
                    throw new CodeZeroException("The key is empty in given json string.");
                }

                if (dictionary.Contains(item.Key))
                {
                    dublicateNames.Add(item.Key);
                }

                dictionary[item.Key] = item.Value.NormalizeLineEndings();
            }

            if (dublicateNames.Count > 0)
            {
                throw new CodeZeroException(
                    "A dictionary can not contain same key twice. There are some duplicated names: " +
                    dublicateNames.JoinAsString(", "));
            }

            return dictionary;
        }
    }
}