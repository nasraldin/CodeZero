//  <copyright file="IMultiTenantLocalizationDictionary.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.Localization.Dictionaries;

namespace CodeZero.Localization
{
    /// <summary>
    /// Extends <see cref="ILocalizationDictionary"/> to add tenant and database based localization.
    /// </summary>
    public interface IMultiTenantLocalizationDictionary : ILocalizationDictionary
    {
        /// <summary>
        /// Gets a <see cref="LocalizedString"/>.
        /// </summary>
        /// <param name="tenantId">TenantId or null for host.</param>
        /// <param name="name">Localization key name.</param>
        LocalizedString GetOrNull(int? tenantId, string name);

        /// <summary>
        /// Gets all <see cref="LocalizedString"/>s.
        /// </summary>
        /// <param name="tenantId">TenantId or null for host.</param>
        IReadOnlyList<LocalizedString> GetAllStrings(int? tenantId);
    }
}