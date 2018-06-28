//  <copyright file="ILdapSettings.cs" project="CodeZero.Identity.Ldap" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;

namespace CodeZero.Identity.Ldap.Configuration
{
    /// <summary>
    /// Used to obtain current values of LDAP settings.
    /// This abstraction allows to define a different source for settings than SettingManager (see default implementation: <see cref="LdapSettings"/>).
    /// </summary>
    public interface ILdapSettings
    {
        Task<bool> GetIsEnabled(int? tenantId);

        Task<ContextType> GetContextType(int? tenantId);

        Task<string> GetContainer(int? tenantId);

        Task<string> GetDomain(int? tenantId);

        Task<string> GetUserName(int? tenantId);

        Task<string> GetPassword(int? tenantId);
    }
}