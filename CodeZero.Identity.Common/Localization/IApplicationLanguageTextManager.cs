//  <copyright file="IApplicationLanguageTextManager.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Globalization;
using System.Threading.Tasks;

namespace CodeZero.Localization
{
    /// <summary>
    /// Manages localization texts for host and tenants.
    /// </summary>
    public interface IApplicationLanguageTextManager
    {
        /// <summary>
        /// Gets a localized string value.
        /// </summary>
        /// <param name="tenantId">TenantId or null for host</param>
        /// <param name="sourceName">Source name</param>
        /// <param name="culture">Culture</param>
        /// <param name="key">Localization key</param>
        /// <param name="tryDefaults">True: fallbacks to default languages if can not find in given culture</param>
        string GetStringOrNull(int? tenantId, string sourceName, CultureInfo culture, string key, bool tryDefaults = true);

        /// <summary>
        /// Updates a localized string value.
        /// </summary>
        /// <param name="tenantId">TenantId or null for host</param>
        /// <param name="sourceName">Source name</param>
        /// <param name="culture">Culture</param>
        /// <param name="key">Localization key</param>
        /// <param name="value">New localized value.</param>
        Task UpdateStringAsync(int? tenantId, string sourceName, CultureInfo culture, string key, string value);
    }
}