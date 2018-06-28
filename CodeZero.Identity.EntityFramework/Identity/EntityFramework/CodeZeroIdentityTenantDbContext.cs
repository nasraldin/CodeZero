//  <copyright file="CodeZeroIdentityTenantDbContext.cs" project="CodeZero.Identity.EntityFramework" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.MultiTenancy;

namespace CodeZero.Identity.EntityFramework
{
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class CodeZeroIdentityTenantDbContext<TRole, TUser> : CodeZeroIdentityCommonDbContext<TRole, TUser>
        where TRole : CodeZeroRole<TUser>
        where TUser : CodeZeroUser<TUser>
    {
        /// <summary>
        /// Default constructor.
        /// Do not directly instantiate this class. Instead, use dependency injection!
        /// </summary>
        protected CodeZeroIdentityTenantDbContext()
        {

        }

        /// <summary>
        /// Constructor with connection string parameter.
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string or a name in connection strings in configuration file</param>
        protected CodeZeroIdentityTenantDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected CodeZeroIdentityTenantDbContext(DbCompiledModel model)
            : base(model)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// </summary>
        protected CodeZeroIdentityTenantDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected CodeZeroIdentityTenantDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        protected CodeZeroIdentityTenantDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        protected CodeZeroIdentityTenantDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }
    }
}