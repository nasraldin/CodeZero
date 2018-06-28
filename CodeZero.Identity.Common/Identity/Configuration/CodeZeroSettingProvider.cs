//  <copyright file="CodeZeroSettingProvider.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.Configuration;
using CodeZero.Localization;

namespace CodeZero.Identity.Configuration
{
    public class CodeZeroSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new List<SettingDefinition>
                   {
                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin,
                           "false",
                           new FixedLocalizableString("Is email confirmation required for login."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.OrganizationUnits.MaxUserMembershipCount,
                           int.MaxValue.ToString(),
                           new FixedLocalizableString("Maximum allowed organization unit membership count for a user."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled,
                           "true",
                           new FixedLocalizableString("Is two factor login enabled."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled,
                           "true",
                           new FixedLocalizableString("Is browser remembering enabled for two factor login."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled,
                           "true",
                           new FixedLocalizableString("Is email provider enabled for two factor login."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled,
                           "true",
                           new FixedLocalizableString("Is sms provider enabled for two factor login."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.UserLockOut.IsEnabled,
                           "true",
                           new FixedLocalizableString("Is user lockout enabled."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout,
                           "5",
                           new FixedLocalizableString("Maxumum Failed access attempt count before user lockout."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds,
                           "300", //5 minutes
                           new FixedLocalizableString("User lockout in seconds."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.PasswordComplexity.RequireDigit,
                           "false",
                           new FixedLocalizableString("Require digit."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.PasswordComplexity.RequireLowercase,
                           "false",
                           new FixedLocalizableString("Require lowercase."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric,
                           "false",
                           new FixedLocalizableString("Require non alphanumeric."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.PasswordComplexity.RequireUppercase,
                           "false",
                           new FixedLocalizableString("Require upper case."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           ),

                       new SettingDefinition(
                           CodeZeroSettingNames.UserManagement.PasswordComplexity.RequiredLength,
                           "3",
                           new FixedLocalizableString("Required length."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()
                           )
                   };
        }
    }
}
