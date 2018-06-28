//  <copyright file="CodeZeroAntiForgeryMvcFilter.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using CodeZero.Dependency;
using CodeZero.Web.Models;
using CodeZero.Web.Mvc.Configuration;
using CodeZero.Web.Mvc.Controllers.Results;
using CodeZero.Web.Mvc.Extensions;
using CodeZero.Web.Mvc.Helpers;
using CodeZero.Web.Security.AntiForgery;
using Castle.Core.Logging;

namespace CodeZero.Web.Mvc.Security.AntiForgery
{
    public class CodeZeroAntiForgeryMvcFilter: IAuthorizationFilter, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly ICodeZeroAntiForgeryManager _CodeZeroAntiForgeryManager;
        private readonly ICodeZeroMvcConfiguration _mvcConfiguration;
        private readonly ICodeZeroAntiForgeryWebConfiguration _antiForgeryWebConfiguration;

        public CodeZeroAntiForgeryMvcFilter(
            ICodeZeroAntiForgeryManager CodeZeroAntiForgeryManager, 
            ICodeZeroMvcConfiguration mvcConfiguration,
            ICodeZeroAntiForgeryWebConfiguration antiForgeryWebConfiguration)
        {
            _CodeZeroAntiForgeryManager = CodeZeroAntiForgeryManager;
            _mvcConfiguration = mvcConfiguration;
            _antiForgeryWebConfiguration = antiForgeryWebConfiguration;
            Logger = NullLogger.Instance;
        }

        public void OnAuthorization(AuthorizationContext context)
        {
            var methodInfo = context.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return;
            }

            var httpVerb = HttpVerbHelper.Create(context.HttpContext.Request.HttpMethod);
            if (!_CodeZeroAntiForgeryManager.ShouldValidate(_antiForgeryWebConfiguration, methodInfo, httpVerb, _mvcConfiguration.IsAutomaticAntiForgeryValidationEnabled))
            {
                return;
            }

            if (!_CodeZeroAntiForgeryManager.IsValid(context.HttpContext))
            {
                CreateErrorResponse(context, methodInfo, "Empty or invalid anti forgery header token.");
            }
        }

        private void CreateErrorResponse(
            AuthorizationContext context, 
            MethodInfo methodInfo, 
            string message)
        {
            Logger.Warn(message);
            Logger.Warn("Requested URI: " + context.HttpContext.Request.Url);

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.StatusDescription = message;

            var isJsonResult = MethodInfoHelper.IsJsonResult(methodInfo);

            if (isJsonResult)
            {
                context.Result = CreateUnAuthorizedJsonResult(message);
            }
            else
            {
                context.Result = CreateUnAuthorizedNonJsonResult(context, message);
            }

            if (isJsonResult || context.HttpContext.Request.IsAjaxRequest())
            {
                context.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            }
        }

        protected virtual CodeZeroJsonResult CreateUnAuthorizedJsonResult(string message)
        {
            return new CodeZeroJsonResult(new AjaxResponse(new ErrorInfo(message), true))
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected virtual HttpStatusCodeResult CreateUnAuthorizedNonJsonResult(AuthorizationContext filterContext, string message)
        {
            return new HttpStatusCodeResult(filterContext.HttpContext.Response.StatusCode, message);
        }
    }
}
