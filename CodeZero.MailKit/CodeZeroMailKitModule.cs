//  <copyright file="CodeZeroMailKitModule.cs" project="CodeZero.MailKit" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;
using CodeZero.Configuration.Startup;
using CodeZero.Modules;
using CodeZero.Net.Mail;
using CodeZero.Reflection.Extensions;

namespace CodeZero.MailKit
{
    [DependsOn(typeof(CodeZeroKernelModule))]
    public class CodeZeroMailKitModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService<IEmailSender, MailKitEmailSender>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroMailKitModule).GetAssembly());
        }
    }
}
