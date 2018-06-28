//  <copyright file="IAuditingHelper.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeZero.Auditing
{
    public interface IAuditingHelper
    {
        bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false);

        AuditInfo CreateAuditInfo(Type type, MethodInfo method, object[] arguments);

        AuditInfo CreateAuditInfo(Type type, MethodInfo method, IDictionary<string, object> arguments);

        void Save(AuditInfo auditInfo);

        Task SaveAsync(AuditInfo auditInfo);
    }
}