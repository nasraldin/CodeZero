//  <copyright file="CodeZeroSettingNames.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Identity.Configuration
{
    public static class CodeZeroSettingNames
    {
        public static class UserManagement
        {
            /// <summary>
            /// "CodeZero.Identity.UserManagement.IsEmailConfirmationRequiredForLogin".
            /// </summary>
            public const string IsEmailConfirmationRequiredForLogin = "CodeZero.Identity.UserManagement.IsEmailConfirmationRequiredForLogin";

            public static class UserLockOut
            {
                /// <summary>
                /// "CodeZero.Identity.UserManagement.UserLockOut.IsEnabled".
                /// </summary>
                public const string IsEnabled = "CodeZero.Identity.UserManagement.UserLockOut.IsEnabled";

                /// <summary>
                /// "CodeZero.Identity.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout".
                /// </summary>
                public const string MaxFailedAccessAttemptsBeforeLockout = "CodeZero.Identity.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout";

                /// <summary>
                /// "CodeZero.Identity.UserManagement.UserLockOut.DefaultAccountLockoutSeconds".
                /// </summary>
                public const string DefaultAccountLockoutSeconds = "CodeZero.Identity.UserManagement.UserLockOut.DefaultAccountLockoutSeconds";
            }

            public static class TwoFactorLogin
            {
                /// <summary>
                /// "CodeZero.Identity.UserManagement.TwoFactorLogin.IsEnabled".
                /// </summary>
                public const string IsEnabled = "CodeZero.Identity.UserManagement.TwoFactorLogin.IsEnabled";

                /// <summary>
                /// "CodeZero.Identity.UserManagement.TwoFactorLogin.IsEmailProviderEnabled".
                /// </summary>
                public const string IsEmailProviderEnabled = "CodeZero.Identity.UserManagement.TwoFactorLogin.IsEmailProviderEnabled";

                /// <summary>
                /// "CodeZero.Identity.UserManagement.TwoFactorLogin.IsSmsProviderEnabled".
                /// </summary>
                public const string IsSmsProviderEnabled = "CodeZero.Identity.UserManagement.TwoFactorLogin.IsSmsProviderEnabled";

                /// <summary>
                /// "CodeZero.Identity.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled".
                /// </summary>
                public const string IsRememberBrowserEnabled = "CodeZero.Identity.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled";
            }

            public static class PasswordComplexity
            {
                /// <summary>
                /// "CodeZero.Identity.UserManagement.PasswordComplexity.RequiredLength"
                /// </summary>
                public const string RequiredLength = "CodeZero.Identity.UserManagement.PasswordComplexity.RequiredLength";

                /// <summary>
                /// "CodeZero.Identity.UserManagement.PasswordComplexity.RequireNonAlphanumeric"
                /// </summary>
                public const string RequireNonAlphanumeric = "CodeZero.Identity.UserManagement.PasswordComplexity.RequireNonAlphanumeric";

                /// <summary>
                /// "CodeZero.Identity.UserManagement.PasswordComplexity.RequireLowercase"
                /// </summary>
                public const string RequireLowercase = "CodeZero.Identity.UserManagement.PasswordComplexity.RequireLowercase";

                /// <summary>
                /// "CodeZero.Identity.UserManagement.PasswordComplexity.RequireUppercase"
                /// </summary>
                public const string RequireUppercase = "CodeZero.Identity.UserManagement.PasswordComplexity.RequireUppercase";

                /// <summary>
                /// "CodeZero.Identity.UserManagement.PasswordComplexity.RequireDigit"
                /// </summary>
                public const string RequireDigit = "CodeZero.Identity.UserManagement.PasswordComplexity.RequireDigit";
            }
        }

        public static class OrganizationUnits
        {
            /// <summary>
            /// "CodeZero.Identity.OrganizationUnits.MaxUserMembershipCount".
            /// </summary>
            public const string MaxUserMembershipCount = "CodeZero.Identity.OrganizationUnits.MaxUserMembershipCount";
        }
    }
}