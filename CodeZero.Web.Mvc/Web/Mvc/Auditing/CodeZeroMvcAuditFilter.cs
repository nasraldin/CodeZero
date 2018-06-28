//  <copyright file="CodeZeroMvcAuditFilter.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Diagnostics;
using System.Web.Mvc;
using CodeZero.Auditing;
using CodeZero.Dependency;
using CodeZero.Web.Mvc.Configuration;
using CodeZero.Web.Mvc.Extensions;

namespace CodeZero.Web.Mvc.Auditing
{
    public class CodeZeroMvcAuditFilter : IActionFilter, ITransientDependency
    {
        private readonly ICodeZeroMvcConfiguration _configuration;
        private readonly IAuditingHelper _auditingHelper;

        public CodeZeroMvcAuditFilter(ICodeZeroMvcConfiguration configuration, IAuditingHelper auditingHelper)
        {
            _configuration = configuration;
            _auditingHelper = auditingHelper;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!ShouldSaveAudit(filterContext))
            {
                CodeZeroAuditFilterData.Set(filterContext.HttpContext, null);
                return;
            }

            var auditInfo = _auditingHelper.CreateAuditInfo(
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerType,
                filterContext.ActionDescriptor.GetMethodInfoOrNull(),
                filterContext.ActionParameters
            );

            var actionStopwatch = Stopwatch.StartNew();

            CodeZeroAuditFilterData.Set(
                filterContext.HttpContext,
                new CodeZeroAuditFilterData(
                    actionStopwatch,
                    auditInfo
                )
            );
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var auditData = CodeZeroAuditFilterData.GetOrNull(filterContext.HttpContext);
            if (auditData == null)
            {
                return;
            }

            auditData.Stopwatch.Stop();

            auditData.AuditInfo.ExecutionDuration = Convert.ToInt32(auditData.Stopwatch.Elapsed.TotalMilliseconds);
            auditData.AuditInfo.Exception = filterContext.Exception;

            _auditingHelper.Save(auditData.AuditInfo);
        }

        private bool ShouldSaveAudit(ActionExecutingContext filterContext)
        {
            var currentMethodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            if (currentMethodInfo == null)
            {
                return false;
            }

            if (_configuration == null)
            {
                return false;
            }

            if (!_configuration.IsAuditingEnabled)
            {
                return false;
            }

            if (filterContext.IsChildAction && !_configuration.IsAuditingEnabledForChildActions)
            {
                return false;
            }

            return _auditingHelper.ShouldSaveAudit(currentMethodInfo, true);
        }
    }
}
