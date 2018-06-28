//  <copyright file="DefaultConfigSettingStore.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using CodeZero.Logging;
using CodeZero.Threading;

namespace CodeZero.Configuration
{
    /// <summary>
    /// Implements default behavior for ISettingStore.
    /// Only <see cref="GetSettingOrNullAsync"/> method is implemented and it gets setting's value
    /// from application's configuration file if exists, or returns null if not.
    /// </summary>
    public class DefaultConfigSettingStore : ISettingStore
    {
        /// <summary>
        /// Gets singleton instance.
        /// </summary>
        public static DefaultConfigSettingStore Instance { get; } = new DefaultConfigSettingStore();
        private DefaultConfigSettingStore()
        {
        }

        public Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                return Task.FromResult<SettingInfo>(null);
            }

            return Task.FromResult(new SettingInfo(tenantId, userId, name, value));

        }
        /// <inheritdoc/>
        public Task DeleteAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support DeleteAsync.");
            return CodeZeroTaskCache.CompletedTask;
        }

        /// <inheritdoc/>
        public Task CreateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support CreateAsync.");
            return CodeZeroTaskCache.CompletedTask;
        }

        /// <inheritdoc/>
        public Task UpdateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support UpdateAsync.");
            return CodeZeroTaskCache.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support GetAllListAsync.");
            return Task.FromResult(new List<SettingInfo>());
        }
    }
}