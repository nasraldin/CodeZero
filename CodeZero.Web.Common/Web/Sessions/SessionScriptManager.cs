//  <copyright file="SessionScriptManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Text;
using CodeZero.Dependency;
using CodeZero.Runtime.Session;

namespace CodeZero.Web.Sessions
{
    public class SessionScriptManager : ISessionScriptManager, ITransientDependency
    {
        public ICodeZeroSession CodeZeroSession { get; set; }

        public SessionScriptManager()
        {
            CodeZeroSession = NullCodeZeroSession.Instance;
        }

        public string GetScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine();

            script.AppendLine("    CodeZero.session = CodeZero.session || {};");
            script.AppendLine("    CodeZero.session.userId = " + (CodeZeroSession.UserId.HasValue ? CodeZeroSession.UserId.Value.ToString() : "null") + ";");
            script.AppendLine("    CodeZero.session.tenantId = " + (CodeZeroSession.TenantId.HasValue ? CodeZeroSession.TenantId.Value.ToString() : "null") + ";");
            script.AppendLine("    CodeZero.session.impersonatorUserId = " + (CodeZeroSession.ImpersonatorUserId.HasValue ? CodeZeroSession.ImpersonatorUserId.Value.ToString() : "null") + ";");
            script.AppendLine("    CodeZero.session.impersonatorTenantId = " + (CodeZeroSession.ImpersonatorTenantId.HasValue ? CodeZeroSession.ImpersonatorTenantId.Value.ToString() : "null") + ";");
            script.AppendLine("    CodeZero.session.multiTenancySide = " + ((int)CodeZeroSession.MultiTenancySide) + ";");

            script.AppendLine();
            script.Append("})();");

            return script.ToString();
        }
    }
}