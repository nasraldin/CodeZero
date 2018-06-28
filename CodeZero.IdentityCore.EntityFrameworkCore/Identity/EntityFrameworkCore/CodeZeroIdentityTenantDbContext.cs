//  <copyright file="CodeZeroIdentityTenantDbContext.cs" project="CodeZero.IdentityCore.EntityFrameworkCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace CodeZero.Identity.EntityFrameworkCore
{
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class CodeZeroTenantDbContext<TRole, TUser, TSelf> : CodeZeroCommonDbContext<TRole, TUser, TSelf>
        where TRole : CodeZeroRole<TUser>
        where TUser : CodeZeroUser<TUser>
        where TSelf : CodeZeroTenantDbContext<TRole, TUser, TSelf>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected CodeZeroTenantDbContext(DbContextOptions<TSelf> options)
            : base(options)
        {

        }
    }
}