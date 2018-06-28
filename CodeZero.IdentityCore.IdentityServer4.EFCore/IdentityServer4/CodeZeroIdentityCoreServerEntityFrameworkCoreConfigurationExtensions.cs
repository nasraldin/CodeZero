//  <copyright file="CodeZeroIdentityCoreServerEntityFrameworkCoreConfigurationExtensions.cs" project="CodeZero.IdentityCore.IdentityServer4.EFCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Microsoft.EntityFrameworkCore;

namespace CodeZero.IdentityServer4
{
    public static class CodeZeroIdentityCoreServerEntityFrameworkCoreConfigurationExtensions
    {
        public static void ConfigurePersistedGrantEntity(this ModelBuilder modelBuilder, string prefix = null, string schemaName = null)
        {
            prefix = prefix ?? "CodeZero";
            var tableName = prefix + "PersistedGrants";

            modelBuilder.Entity<PersistedGrantEntity>(grant =>
            {
                if (schemaName == null)
                {
                    grant.ToTable(tableName);
                }
                else
                {
                    grant.ToTable(tableName, schemaName);
                }
                grant.Property(x => x.Id).HasMaxLength(200).ValueGeneratedNever();
                grant.Property(x => x.Type).HasMaxLength(50).IsRequired();
                grant.Property(x => x.SubjectId).HasMaxLength(200);
                grant.Property(x => x.ClientId).HasMaxLength(200).IsRequired();
                grant.Property(x => x.CreationTime).IsRequired();
                // 50000 chosen to be explicit to allow enough size to avoid truncation, yet stay beneath the MySql row size limit of ~65K
                // apparently anything over 4K converts to nvarchar(max) on SqlServer
                grant.Property(x => x.Data).HasMaxLength(50000).IsRequired();

                grant.HasKey(x => x.Id);

                grant.HasIndex(x => new { x.SubjectId, x.ClientId, x.Type });
            });
        }
    }
}