//  <copyright file="IMultiTenantLocalizationSource.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Globalization;
using CodeZero.Localization.Sources;

namespace CodeZero.Localization
{
    /// <summary>
    /// Extends <see cref="ILocalizationSource"/> to add tenant and database based localization.
    /// </summary>
    public interface IMultiTenantLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// Gets a <see cref="LocalizedString"/>.
        /// </summary>
        /// <param name="tenantId">TenantId or null for host.</param>
        /// <param name="name">Localization key name.</param>
        /// <param name="culture">Culture</param>
        string GetString(int? tenantId, string name, CultureInfo culture);

        /// <summary>
        /// Gets a <see cref="LocalizedString"/>.
        /// </summary>
        /// <param name="tenantId">TenantId or null for host.</param>
        /// <param name="name">Localization key name.</param>
        /// <param name="culture">Culture</param>
        /// <param name="tryDefaults">True: fallbacks to default languages if can not find in given culture</param>
        string GetStringOrNull(int? tenantId, string name, CultureInfo culture, bool tryDefaults = true);
    }
}