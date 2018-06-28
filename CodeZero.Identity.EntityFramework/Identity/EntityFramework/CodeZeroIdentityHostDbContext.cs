//  <copyright file="CodeZeroIdentityHostDbContext.cs" project="CodeZero.Identity.EntityFramework" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using CodeZero.Application.Editions;
using CodeZero.Application.Features;
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.BackgroundJobs;
using CodeZero.MultiTenancy;

namespace CodeZero.Identity.EntityFramework
{
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class CodeZeroIdentityHostDbContext<TTenant, TRole, TUser> : CodeZeroIdentityCommonDbContext<TRole, TUser>
        where TTenant : CodeZeroTenant<TUser>
        where TRole : CodeZeroRole<TUser>
        where TUser : CodeZeroUser<TUser>
    {
        /// <summary>
        /// Tenants
        /// </summary>
        public virtual IDbSet<TTenant> Tenants { get; set; }

        /// <summary>
        /// Editions.
        /// </summary>
        public virtual IDbSet<Edition> Editions { get; set; }

        /// <summary>
        /// FeatureSettings.
        /// </summary>
        public virtual IDbSet<FeatureSetting> FeatureSettings { get; set; }

        /// <summary>
        /// TenantFeatureSetting.
        /// </summary>
        public virtual IDbSet<TenantFeatureSetting> TenantFeatureSettings { get; set; }

        /// <summary>
        /// EditionFeatureSettings.
        /// </summary>
        public virtual IDbSet<EditionFeatureSetting> EditionFeatureSettings { get; set; }

        /// <summary>
        /// Background jobs.
        /// </summary>
        public virtual IDbSet<BackgroundJobInfo> BackgroundJobs { get; set; }

        /// <summary>
        /// User accounts
        /// </summary>
        public virtual IDbSet<UserAccount> UserAccounts { get; set; }

        protected CodeZeroIdentityHostDbContext()
        {

        }

        protected CodeZeroIdentityHostDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected CodeZeroIdentityHostDbContext(DbCompiledModel model)
            : base(model)
        {

        }

        protected CodeZeroIdentityHostDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected CodeZeroIdentityHostDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        protected CodeZeroIdentityHostDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        protected CodeZeroIdentityHostDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }
    }
}