//  <copyright file="CodeZeroIdentityDbModelBuilderExtensions.cs" project="CodeZero.IdentityCore.EntityFrameworkCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Application.Editions;
using CodeZero.Application.Features;
using CodeZero.Auditing;
using CodeZero.Authorization;
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.BackgroundJobs;
using CodeZero.Configuration;
using CodeZero.EntityHistory;
using CodeZero.Localization;
using CodeZero.MultiTenancy;
using CodeZero.Notifications;
using CodeZero.Organizations;
using Microsoft.EntityFrameworkCore;

namespace CodeZero.Identity.EntityFrameworkCore
{
    /// <summary>
    /// Extension methods for <see cref="DbModelBuilder"/>.
    /// </summary>
    public static class CodeZeroDbModelBuilderExtensions
    {
        /// <summary>
        /// Changes prefix for CodeZero tables (which is "CodeZero" by default).
        /// Can be null/empty string to clear the prefix.
        /// </summary>
        /// <typeparam name="TTenant">The type of the tenant entity.</typeparam>
        /// <typeparam name="TRole">The type of the role entity.</typeparam>
        /// <typeparam name="TUser">The type of the user entity.</typeparam>
        /// <param name="modelBuilder">Model builder.</param>
        /// <param name="prefix">Table prefix, or null to clear prefix.</param>
        public static void ChangeCodeZeroTablePrefix<TTenant, TRole, TUser>(this ModelBuilder modelBuilder, string prefix, string schemaName = null)
            where TTenant : CodeZeroTenant<TUser>
            where TRole : CodeZeroRole<TUser>
            where TUser : CodeZeroUser<TUser>
        {
            prefix = prefix ?? "";

            SetTableName<AuditLog>(modelBuilder, prefix + "AuditLogs", schemaName);
            SetTableName<BackgroundJobInfo>(modelBuilder, prefix + "BackgroundJobs", schemaName);
            SetTableName<Edition>(modelBuilder, prefix + "Editions", schemaName);
            SetTableName<EntityChange>(modelBuilder, prefix + "EntityChanges", schemaName);
            SetTableName<EntityChangeSet>(modelBuilder, prefix + "EntityChangeSets", schemaName);
            SetTableName<EntityPropertyChange>(modelBuilder, prefix + "EntityPropertyChanges", schemaName);
            SetTableName<FeatureSetting>(modelBuilder, prefix + "Features", schemaName);
            SetTableName<TenantFeatureSetting>(modelBuilder, prefix + "Features", schemaName);
            SetTableName<EditionFeatureSetting>(modelBuilder, prefix + "Features", schemaName);
            SetTableName<ApplicationLanguage>(modelBuilder, prefix + "Languages", schemaName);
            SetTableName<ApplicationLanguageText>(modelBuilder, prefix + "LanguageTexts", schemaName);
            SetTableName<NotificationInfo>(modelBuilder, prefix + "Notifications", schemaName);
            SetTableName<NotificationSubscriptionInfo>(modelBuilder, prefix + "NotificationSubscriptions", schemaName);
            SetTableName<OrganizationUnit>(modelBuilder, prefix + "OrganizationUnits", schemaName);
            SetTableName<PermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            SetTableName<RolePermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            SetTableName<UserPermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            SetTableName<TRole>(modelBuilder, prefix + "Roles", schemaName);
            SetTableName<Setting>(modelBuilder, prefix + "Settings", schemaName);
            SetTableName<TTenant>(modelBuilder, prefix + "Tenants", schemaName);
            SetTableName<UserLogin>(modelBuilder, prefix + "UserLogins", schemaName);
            SetTableName<UserLoginAttempt>(modelBuilder, prefix + "UserLoginAttempts", schemaName);
            SetTableName<TenantNotificationInfo>(modelBuilder, prefix + "TenantNotifications", schemaName);
            SetTableName<UserNotificationInfo>(modelBuilder, prefix + "UserNotifications", schemaName);
            SetTableName<UserOrganizationUnit>(modelBuilder, prefix + "UserOrganizationUnits", schemaName);
            SetTableName<UserRole>(modelBuilder, prefix + "UserRoles", schemaName);
            SetTableName<TUser>(modelBuilder, prefix + "Users", schemaName);
            SetTableName<UserAccount>(modelBuilder, prefix + "UserAccounts", schemaName);
            SetTableName<UserClaim>(modelBuilder, prefix + "UserClaims", schemaName);
            SetTableName<RoleClaim>(modelBuilder, prefix + "RoleClaims", schemaName);
            SetTableName<UserToken>(modelBuilder, prefix + "UserTokens", schemaName);
        }

        internal static void SetTableName<TEntity>(this ModelBuilder modelBuilder, string tableName, string schemaName)
            where TEntity : class
        {
            if (schemaName == null)
            {
                modelBuilder.Entity<TEntity>().ToTable(tableName);
            }
            else
            {
                modelBuilder.Entity<TEntity>().ToTable(tableName, schemaName);
            }
        }
    }
}