//  <copyright file="IdentityLdapConfigurationExtensions.cs" project="CodeZero.Identity.Ldap" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;

namespace CodeZero.Identity.Ldap.Configuration
{
    /// <summary>
    /// Extension methods for module zero configurations.
    /// </summary>
    public static class IdentityLdapConfigurationExtensions
    {
        /// <summary>
        /// Configures CodeZero Zero LDAP module.
        /// </summary>
        /// <returns></returns>
        public static ICodeZeroLdapModuleConfig ZeroLdap(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.CodeZeroConfiguration.Get<ICodeZeroLdapModuleConfig>();
        }
    }
}
