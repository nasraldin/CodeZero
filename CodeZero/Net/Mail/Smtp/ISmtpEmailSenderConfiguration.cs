//  <copyright file="ISmtpEmailSenderConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Net.Mail.Smtp
{
    /// <summary>
    /// Defines configurations to used by SmtpClient object.
    /// </summary>
    public interface ISmtpEmailSenderConfiguration : IEmailSenderConfiguration
    {
        /// <summary>
        /// SMTP Host name/IP.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// SMTP Port.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// User name to login to SMTP server.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Password to login to SMTP server.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Domain name to login to SMTP server.
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Is SSL enabled?
        /// </summary>
        bool EnableSsl { get; }

        /// <summary>
        /// Use default credentials?
        /// </summary>
        bool UseDefaultCredentials { get; }
    }
}