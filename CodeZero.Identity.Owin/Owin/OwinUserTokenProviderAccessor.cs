//  <copyright file="OwinUserTokenProviderAccessor.cs" project="CodeZero.Identity.Owin" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Authorization.Users;
using Castle.Core.Logging;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace CodeZero.Owin
{
    public class OwinUserTokenProviderAccessor : IUserTokenProviderAccessor
    {
        public ILogger Logger { get; set; }

        public IDataProtectionProvider DataProtectionProvider { get; set; }

        public OwinUserTokenProviderAccessor()
        {
            Logger = NullLogger.Instance;
        }

        public IUserTokenProvider<TUser, long> GetUserTokenProviderOrNull<TUser>()
            where TUser : CodeZeroUser<TUser>
        {
            if (DataProtectionProvider == null)
            {
                Logger.Debug("DataProtectionProvider has not been set yet.");
                return null;
            }

            return new DataProtectorTokenProvider<TUser, long>(DataProtectionProvider.Create("ASP.NET Identity"));
        }
    }
}