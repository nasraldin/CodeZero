//  <copyright file="EmailSenderConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Configuration;
using CodeZero.Extensions;

namespace CodeZero.Net.Mail
{
    /// <summary>
    /// Implementation of <see cref="IEmailSenderConfiguration"/> that reads settings
    /// from <see cref="ISettingManager"/>.
    /// </summary>
    public abstract class EmailSenderConfiguration : IEmailSenderConfiguration
    {
        public virtual string DefaultFromAddress
        {
            get { return GetNotEmptySettingValue(EmailSettingNames.DefaultFromAddress); }
        }

        public virtual string DefaultFromDisplayName
        {
            get { return SettingManager.GetSettingValue(EmailSettingNames.DefaultFromDisplayName); }
        }

        protected readonly ISettingManager SettingManager;

        /// <summary>
        /// Creates a new <see cref="EmailSenderConfiguration"/>.
        /// </summary>
        protected EmailSenderConfiguration(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }

        /// <summary>
        /// Gets a setting value by checking. Throws <see cref="CodeZeroException"/> if it's null or empty.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        /// <returns>Value of the setting</returns>
        protected string GetNotEmptySettingValue(string name)
        {
            var value = SettingManager.GetSettingValue(name);

            if (value.IsNullOrEmpty())
            {
                throw new CodeZeroException($"Setting value for '{name}' is null or empty!");
            }

            return value;
        }
    }
}