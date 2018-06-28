//  <copyright file="IUserTokenProviderAccessor.cs" project="CodeZero.Identity" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Microsoft.AspNet.Identity;

namespace CodeZero.Authorization.Users
{
    public interface IUserTokenProviderAccessor
    {
        IUserTokenProvider<TUser, long> GetUserTokenProviderOrNull<TUser>() 
            where TUser : CodeZeroUser<TUser>;
    }
}