//  <copyright file="IdentityEmailService.cs" project="CodeZero.Identity" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Dependency;
using CodeZero.Net.Mail;
using Microsoft.AspNet.Identity;

namespace CodeZero.IdentityFramework
{
    public class IdentityEmailMessageService : IIdentityMessageService, ITransientDependency
    {
        private readonly IEmailSender _emailSender;

        public IdentityEmailMessageService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public virtual Task SendAsync(IdentityMessage message)
        {
            return _emailSender.SendAsync(message.Destination, message.Subject, message.Body);
        }
    }
}
