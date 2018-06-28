//  <copyright file="NullEmailSender.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Net.Mail;
using System.Threading.Tasks;
using Castle.Core.Logging;

namespace CodeZero.Net.Mail
{
    /// <summary>
    /// This class is an implementation of <see cref="IEmailSender"/> as similar to null pattern.
    /// It does not send emails but logs them.
    /// </summary>
    public class NullEmailSender : EmailSenderBase
    {
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a new <see cref="NullEmailSender"/> object.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public NullEmailSender(IEmailSenderConfiguration configuration)
            : base(configuration)
        {
            Logger = NullLogger.Instance;
        }

        protected override Task SendEmailAsync(MailMessage mail)
        {
            Logger.Warn("USING NullEmailSender!");
            Logger.Debug("SendEmailAsync:");
            LogEmail(mail);
            return Task.FromResult(0);
        }

        protected override void SendEmail(MailMessage mail)
        {
            Logger.Warn("USING NullEmailSender!");
            Logger.Debug("SendEmail:");
            LogEmail(mail);
        }

        private void LogEmail(MailMessage mail)
        {
            Logger.Debug(mail.To.ToString());
            Logger.Debug(mail.CC.ToString());
            Logger.Debug(mail.Subject);
            Logger.Debug(mail.Body);
        }
    }
}