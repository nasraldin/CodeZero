//  <copyright file="LdapSettingProvider.cs" project="CodeZero.Identity.Ldap" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using CodeZero.Configuration;
using CodeZero.Localization;

namespace CodeZero.Identity.Ldap.Configuration
{
    /// <summary>
    /// Defines LDAP settings.
    /// </summary>
    public class LdapSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       new SettingDefinition(LdapSettingNames.IsEnabled, "false", L("Ldap_IsEnabled"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.ContextType, ContextType.Domain.ToString(), L("Ldap_ContextType"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.Container, null, L("Ldap_Container"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.Domain, null, L("Ldap_Domain"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.UserName, null, L("Ldap_UserName"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(LdapSettingNames.Password, null, L("Ldap_Password"), scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false)
                   };
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CodeZeroConsts.LocalizationSourceName);
        }
    }
}
