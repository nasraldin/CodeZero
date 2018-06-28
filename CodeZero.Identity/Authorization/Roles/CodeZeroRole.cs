//  <copyright file="CodeZeroRole.cs" project="CodeZero.Identity" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Authorization.Users;
using CodeZero.Domain.Entities.Auditing;
using Microsoft.AspNet.Identity;

namespace CodeZero.Authorization.Roles
{
    /// <summary>
    /// Represents a role in an application. A role is used to group permissions.
    /// </summary>
    /// <remarks> 
    /// Application should use permissions to check if user is granted to perform an operation.
    /// Checking 'if a user has a role' is not possible until the role is static (<see cref="CodeZeroRoleBase.IsStatic"/>).
    /// Static roles can be used in the code and can not be deleted by users.
    /// Non-static (dynamic) roles can be added/removed by users and we can not know their name while coding.
    /// A user can have multiple roles. Thus, user will have all permissions of all assigned roles.
    /// </remarks>
    public abstract class CodeZeroRole<TUser> : CodeZeroRoleBase, IRole<int>, IFullAudited<TUser>
        where TUser : CodeZeroUser<TUser>
    {
        public virtual TUser DeleterUser { get; set; }

        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }

        protected CodeZeroRole()
        {
        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroRole{TUser}"/> object.
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="displayName">Display name of the role</param>
        protected CodeZeroRole(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {
        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroRole{TUser}"/> object.
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="name">Unique role name</param>
        /// <param name="displayName">Display name of the role</param>
        protected CodeZeroRole(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {
        }
    }
}