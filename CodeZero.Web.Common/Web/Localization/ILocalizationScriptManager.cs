//  <copyright file="ILocalizationScriptManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Globalization;

namespace CodeZero.Web.Localization
{
    /// <summary>
    /// Define interface to get localization JavaScript.
    /// </summary>
    public interface ILocalizationScriptManager
    {
        /// <summary>
        /// Gets JavaScript that contains all localization information in current culture.
        /// </summary>
        string GetScript();

        /// <summary>
        /// Gets JavaScript that contains all localization information in given culture.
        /// </summary>
        /// <param name="cultureInfo">Culture to get script</param>
        string GetScript(CultureInfo cultureInfo);
    }
}
