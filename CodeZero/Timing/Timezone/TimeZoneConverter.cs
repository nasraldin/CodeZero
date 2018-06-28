//  <copyright file="TimeZoneConverter.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Configuration;
using CodeZero.Dependency;

namespace CodeZero.Timing.Timezone
{
    /// <summary>
    /// Time zone converter class
    /// </summary>
    public class TimeZoneConverter : ITimeZoneConverter, ITransientDependency
    {
        private readonly ISettingManager _settingManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settingManager"></param>
        public TimeZoneConverter(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        /// <inheritdoc/>
        public DateTime? Convert(DateTime? date, int? tenantId, long userId)
        {
            if (!date.HasValue)
            {
                return null;
            }

            if (!Clock.SupportsMultipleTimezone)
            {
                return date;
            }

            var usersTimezone = _settingManager.GetSettingValueForUser(TimingSettingNames.TimeZone, tenantId, userId);
            if(string.IsNullOrEmpty(usersTimezone))
            {
                return date;
            }
            
            return TimezoneHelper.ConvertFromUtc(date.Value.ToUniversalTime(), usersTimezone);
        }

        /// <inheritdoc/>
        public DateTime? Convert(DateTime? date, int tenantId)
        {
            if (!date.HasValue)
            {
                return null;
            }

            if (!Clock.SupportsMultipleTimezone)
            {
                return date;
            }

            var tenantsTimezone = _settingManager.GetSettingValueForTenant(TimingSettingNames.TimeZone, tenantId);
            if (string.IsNullOrEmpty(tenantsTimezone))
            {
                return date;
            }

            return TimezoneHelper.ConvertFromUtc(date.Value.ToUniversalTime(), tenantsTimezone);
        }
        
        /// <inheritdoc/>
        public DateTime? Convert(DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            if (!Clock.SupportsMultipleTimezone)
            {
                return date;
            }

            var applicationsTimezone = _settingManager.GetSettingValueForApplication(TimingSettingNames.TimeZone);
            if (string.IsNullOrEmpty(applicationsTimezone))
            {
                return date;
            }

            return TimezoneHelper.ConvertFromUtc(date.Value.ToUniversalTime(), applicationsTimezone);
        }
    }
}