//  <copyright file="CodeZeroAuditFilterData.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using CodeZero.Auditing;

namespace CodeZero.Web.Mvc.Auditing
{
    public class CodeZeroAuditFilterData
    {
        private const string CodeZeroAuditFilterDataHttpContextKey = "__CodeZeroAuditFilterData";

        public Stopwatch Stopwatch { get; }

        public AuditInfo AuditInfo { get; }

        public CodeZeroAuditFilterData(
            Stopwatch stopwatch,
            AuditInfo auditInfo)
        {
            Stopwatch = stopwatch;
            AuditInfo = auditInfo;
        }

        public static void Set(HttpContextBase httpContext, CodeZeroAuditFilterData auditFilterData)
        {
            GetAuditDataStack(httpContext).Push(auditFilterData);
        }

        public static CodeZeroAuditFilterData GetOrNull(HttpContextBase httpContext)
        {
            var stack = GetAuditDataStack(httpContext);
            return stack.Count <= 0
                ? null
                : stack.Pop();
        }

        private static Stack<CodeZeroAuditFilterData> GetAuditDataStack(HttpContextBase httpContext)
        {
            var stack = httpContext.Items[CodeZeroAuditFilterDataHttpContextKey] as Stack<CodeZeroAuditFilterData>;

            if (stack == null)
            {
                stack = new Stack<CodeZeroAuditFilterData>();
                httpContext.Items[CodeZeroAuditFilterDataHttpContextKey] = stack;
            }

            return stack;
        }
    }
}