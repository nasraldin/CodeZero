//  <copyright file="CodeZeroApiValidationFilter.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CodeZero.Dependency;
using CodeZero.WebApi.Configuration;

namespace CodeZero.WebApi.Validation
{
    public class CodeZeroApiValidationFilter : IActionFilter, ITransientDependency
    {
        public bool AllowMultiple => false;

        private readonly IIocResolver _iocResolver;
        private readonly ICodeZeroWebApiConfiguration _configuration;

        public CodeZeroApiValidationFilter(IIocResolver iocResolver, ICodeZeroWebApiConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (!_configuration.IsValidationEnabledForControllers)
            {
                return await continuation();
            }

            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            /* ModelState.IsValid is being checked to handle parameter binding errors (ex: send string for an int value).
             * These type of errors can not be catched from application layer. */

            if (actionContext.ModelState.IsValid
                && actionContext.ActionDescriptor.IsDynamicCodeZeroAction())
            {
                return await continuation();
            }

            using (var validator = _iocResolver.ResolveAsDisposable<WebApiActionInvocationValidator>())
            {
                validator.Object.Initialize(actionContext, methodInfo);
                validator.Object.Validate();
            }

            return await continuation();
        }
    }
}