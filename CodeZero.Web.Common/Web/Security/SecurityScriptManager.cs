//  <copyright file="SecurityScriptManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Text;
using CodeZero.Dependency;
using CodeZero.Web.Security.AntiForgery;

namespace CodeZero.Web.Security
{
    internal class SecurityScriptManager : ISecurityScriptManager, ITransientDependency
    {
        private readonly ICodeZeroAntiForgeryConfiguration _CodeZeroAntiForgeryConfiguration;

        public SecurityScriptManager(ICodeZeroAntiForgeryConfiguration CodeZeroAntiForgeryConfiguration)
        {
            _CodeZeroAntiForgeryConfiguration = CodeZeroAntiForgeryConfiguration;
        }

        public string GetScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine("    CodeZero.security.antiForgery.tokenCookieName = '" + _CodeZeroAntiForgeryConfiguration.TokenCookieName + "';");
            script.AppendLine("    CodeZero.security.antiForgery.tokenHeaderName = '" + _CodeZeroAntiForgeryConfiguration.TokenHeaderName + "';");
            script.Append("})();");

            return script.ToString();
        }
    }
}
