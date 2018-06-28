//  <copyright file="ICodeZeroIdentityAspNetCoreConfiguration.cs" project="CodeZero.Identity.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Identity.AspNetCore
{
    public interface ICodeZeroIdentityAspNetCoreConfiguration
    {
        /// <summary>
        /// Authentication scheme of the application.
        /// </summary>
        string AuthenticationScheme { get; set; }

        /// <summary>
        /// Default value: <see cref="Microsoft.AspNet.Identity.DefaultAuthenticationTypes.TwoFactorCookie"/>.
        /// </summary>
        string TwoFactorAuthenticationScheme { get; set; }

        /// <summary>
        /// Default value: <see cref="Microsoft.AspNet.Identity.DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie"/>.
        /// </summary>
        string TwoFactorRememberBrowserAuthenticationScheme { get; set; }
    }
}