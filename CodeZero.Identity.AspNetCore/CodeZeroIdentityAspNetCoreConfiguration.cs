//  <copyright file="CodeZeroIdentityAspNetCoreConfiguration.cs" project="CodeZero.Identity.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Microsoft.AspNet.Identity;

namespace CodeZero.Identity.AspNetCore
{
    internal class CodeZeroIdentityAspNetCoreConfiguration : ICodeZeroIdentityAspNetCoreConfiguration
    {
        public string AuthenticationScheme { get; set; }

        public string TwoFactorAuthenticationScheme { get; set; }

        public string TwoFactorRememberBrowserAuthenticationScheme { get; set; }

        public CodeZeroIdentityAspNetCoreConfiguration()
        {
            AuthenticationScheme = "AppAuthenticationScheme";
            TwoFactorAuthenticationScheme = DefaultAuthenticationTypes.TwoFactorCookie;
            TwoFactorRememberBrowserAuthenticationScheme = DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie;
        }
    }
}
