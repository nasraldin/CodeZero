//  <copyright file="CodeZeroAntiForgeryApiFilter.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CodeZero.Dependency;
using CodeZero.Web.Security.AntiForgery;
using CodeZero.WebApi.Configuration;
using CodeZero.WebApi.Controllers.Dynamic.Selectors;
using CodeZero.WebApi.Validation;
using Castle.Core.Logging;

namespace CodeZero.WebApi.Security.AntiForgery
{
    public class CodeZeroAntiForgeryApiFilter : IAuthorizationFilter, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public bool AllowMultiple => false;

        private readonly ICodeZeroAntiForgeryManager _CodeZeroAntiForgeryManager;
        private readonly ICodeZeroWebApiConfiguration _webApiConfiguration;
        private readonly ICodeZeroAntiForgeryWebConfiguration _antiForgeryWebConfiguration;

        public CodeZeroAntiForgeryApiFilter(
            ICodeZeroAntiForgeryManager CodeZeroAntiForgeryManager, 
            ICodeZeroWebApiConfiguration webApiConfiguration,
            ICodeZeroAntiForgeryWebConfiguration antiForgeryWebConfiguration)
        {
            _CodeZeroAntiForgeryManager = CodeZeroAntiForgeryManager;
            _webApiConfiguration = webApiConfiguration;
            _antiForgeryWebConfiguration = antiForgeryWebConfiguration;
            Logger = NullLogger.Instance;
        }

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            if (!_CodeZeroAntiForgeryManager.ShouldValidate(_antiForgeryWebConfiguration, methodInfo, actionContext.Request.Method.ToHttpVerb(), _webApiConfiguration.IsAutomaticAntiForgeryValidationEnabled))
            {
                return await continuation();
            }

            if (!_CodeZeroAntiForgeryManager.IsValid(actionContext.Request.Headers))
            {
                return CreateErrorResponse(actionContext, "Empty or invalid anti forgery header token.");
            }

            return await continuation();
        }

        protected virtual HttpResponseMessage CreateErrorResponse(HttpActionContext actionContext, string reason)
        {
            Logger.Warn(reason);
            Logger.Warn("Requested URI: " + actionContext.Request.RequestUri);
            return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = reason };
        }
    }
}