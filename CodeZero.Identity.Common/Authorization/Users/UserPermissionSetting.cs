//  <copyright file="UserPermissionSetting.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Authorization.Users
{
    /// <summary>
    /// Used to store setting for a permission for a user.
    /// </summary>
    public class UserPermissionSetting : PermissionSetting
    {
        /// <summary>
        /// User id.
        /// </summary>
        public virtual long UserId { get; set; }
    }
}