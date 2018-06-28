//  <copyright file="EmailSettingNames.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Net.Mail
{
    /// <summary>
    /// Declares names of the settings defined by <see cref="EmailSettingProvider"/>.
    /// </summary>
    public static class EmailSettingNames
    {
        /// <summary>
        /// CodeZero.Net.Mail.DefaultFromAddress
        /// </summary>
        public const string DefaultFromAddress = "CodeZero.Net.Mail.DefaultFromAddress";

        /// <summary>
        /// CodeZero.Net.Mail.DefaultFromDisplayName
        /// </summary>
        public const string DefaultFromDisplayName = "CodeZero.Net.Mail.DefaultFromDisplayName";

        /// <summary>
        /// SMTP related email settings.
        /// </summary>
        public static class Smtp
        {
            /// <summary>
            /// CodeZero.Net.Mail.Smtp.Host
            /// </summary>
            public const string Host = "CodeZero.Net.Mail.Smtp.Host";

            /// <summary>
            /// CodeZero.Net.Mail.Smtp.Port
            /// </summary>
            public const string Port = "CodeZero.Net.Mail.Smtp.Port";

            /// <summary>
            /// CodeZero.Net.Mail.Smtp.UserName
            /// </summary>
            public const string UserName = "CodeZero.Net.Mail.Smtp.UserName";

            /// <summary>
            /// CodeZero.Net.Mail.Smtp.Password
            /// </summary>
            public const string Password = "CodeZero.Net.Mail.Smtp.Password";

            /// <summary>
            /// CodeZero.Net.Mail.Smtp.Domain
            /// </summary>
            public const string Domain = "CodeZero.Net.Mail.Smtp.Domain";

            /// <summary>
            /// CodeZero.Net.Mail.Smtp.EnableSsl
            /// </summary>
            public const string EnableSsl = "CodeZero.Net.Mail.Smtp.EnableSsl";

            /// <summary>
            /// CodeZero.Net.Mail.Smtp.UseDefaultCredentials
            /// </summary>
            public const string UseDefaultCredentials = "CodeZero.Net.Mail.Smtp.UseDefaultCredentials";
        }
    }
}