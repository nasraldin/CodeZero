//  <copyright file="CodeZeroUser.cs" project="CodeZero.Identity" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Domain.Entities.Auditing;
using Microsoft.AspNet.Identity;

namespace CodeZero.Authorization.Users
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public abstract class CodeZeroUser<TUser> : CodeZeroUserBase, IUser<long>, IFullAudited<TUser>
        where TUser : CodeZeroUser<TUser>
    {
        public virtual TUser DeleterUser { get; set; }

        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }
    }
}