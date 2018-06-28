//  <copyright file="CustomValidationContextExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Localization;

namespace CodeZero.Runtime.Validation
{
    public static class CustomValidationContextExtensions
    {
        /// <param name="validationContext">Validation context</param>
        /// <param name="sourceName">Localization source name</param>
        /// <param name="key">Localization key</param>
        public static string Localize(this CustomValidationContext validationContext, string sourceName, string key)
        {
            var localizationManager = validationContext.IocResolver.Resolve<ILocalizationManager>();
            var source = localizationManager.GetSource(sourceName);
            return source.GetString(key);
        }
    }
}