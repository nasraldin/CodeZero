//  <copyright file="LdapSettings.cs" project="CodeZero.Identity.Ldap" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using CodeZero.Configuration;
using CodeZero.Dependency;
using CodeZero.Extensions;

namespace CodeZero.Identity.Ldap.Configuration
{
    /// <summary>
    /// Implements <see cref="ILdapSettings"/> to get settings from <see cref="ISettingManager"/>.
    /// </summary>
    
    public class LdapSettings : ILdapSettings, ITransientDependency
    {
        protected ISettingManager SettingManager { get; }

        public LdapSettings(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }

        public virtual Task<bool> GetIsEnabled(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync<bool>(LdapSettingNames.IsEnabled, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync<bool>(LdapSettingNames.IsEnabled);
        }

        public virtual async Task<ContextType> GetContextType(int? tenantId)
        {
            return tenantId.HasValue
                ? (await SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.ContextType, tenantId.Value)).ToEnum<ContextType>()
                : (await SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.ContextType)).ToEnum<ContextType>();
        }

        public virtual Task<string> GetContainer(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.Container, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.Container);
        }

        public virtual Task<string> GetDomain(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.Domain, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.Domain);
        }

        public virtual Task<string> GetUserName(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.UserName, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.UserName);
        }

        public virtual Task<string> GetPassword(int? tenantId)
        {
            return tenantId.HasValue
                ? SettingManager.GetSettingValueForTenantAsync(LdapSettingNames.Password, tenantId.Value)
                : SettingManager.GetSettingValueForApplicationAsync(LdapSettingNames.Password);
        }
    }
}