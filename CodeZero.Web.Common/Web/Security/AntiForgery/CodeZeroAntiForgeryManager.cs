//  <copyright file="CodeZeroAntiForgeryManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Dependency;
using Castle.Core.Logging;

namespace CodeZero.Web.Security.AntiForgery
{
    public class CodeZeroAntiForgeryManager : ICodeZeroAntiForgeryManager, ICodeZeroAntiForgeryValidator, ITransientDependency
    {
        public ILogger Logger { protected get; set; }

        public ICodeZeroAntiForgeryConfiguration Configuration { get; }

        public CodeZeroAntiForgeryManager(ICodeZeroAntiForgeryConfiguration configuration)
        {
            Configuration = configuration;
            Logger = NullLogger.Instance;
        }

        public virtual string GenerateToken()
        {
            return Guid.NewGuid().ToString("D");
        }

        public virtual bool IsValid(string cookieValue, string tokenValue)
        {
            return cookieValue == tokenValue;
        }
    }
}