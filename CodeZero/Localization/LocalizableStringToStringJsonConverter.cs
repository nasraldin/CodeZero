//  <copyright file="LocalizableStringToStringJsonConverter.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Reflection;
using Newtonsoft.Json;

namespace CodeZero.Localization
{
    /// <summary>
    /// This class can be used to serialize <see cref="ILocalizableString"/> to <see cref="string"/> during serialization.
    /// It does not work for deserialization.
    /// </summary>
    public class LocalizableStringToStringJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var localizableString = (ILocalizableString) value;
            writer.WriteValue(localizableString.Localize(new LocalizationContext(LocalizationHelper.Manager)));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (ILocalizableString).GetTypeInfo().IsAssignableFrom(objectType);
        }
    }
}