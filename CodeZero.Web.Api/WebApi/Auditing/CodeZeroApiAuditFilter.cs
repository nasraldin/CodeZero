//  <copyright file="CodeZeroApiAuditFilter.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CodeZero.Auditing;
using CodeZero.Dependency;
using CodeZero.WebApi.Validation;

namespace CodeZero.WebApi.Auditing
{
    public class CodeZeroApiAuditFilter : IActionFilter, ITransientDependency
    {
        public bool AllowMultiple => false;

        private readonly IAuditingHelper _auditingHelper;

        public CodeZeroApiAuditFilter(IAuditingHelper auditingHelper)
        {
            _auditingHelper = auditingHelper;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var method = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (method == null || !ShouldSaveAudit(actionContext))
            {
                return await continuation();
            }

            var auditInfo = _auditingHelper.CreateAuditInfo(
                actionContext.ActionDescriptor.ControllerDescriptor.ControllerType,
                method,
                actionContext.ActionArguments
            );

            var stopwatch = Stopwatch.StartNew();

            try
            {
                return await continuation();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                await _auditingHelper.SaveAsync(auditInfo);
            }
        }

        private bool ShouldSaveAudit(HttpActionContext context)
        {
            if (context.ActionDescriptor.IsDynamicCodeZeroAction())
            {
                return false;
            }

            return _auditingHelper.ShouldSaveAudit(context.ActionDescriptor.GetMethodInfoOrNull(), true);
        }
    }
}