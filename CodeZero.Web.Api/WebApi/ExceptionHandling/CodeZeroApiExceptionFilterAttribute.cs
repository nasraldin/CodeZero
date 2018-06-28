//  <copyright file="CodeZeroApiExceptionFilterAttribute.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using CodeZero.Dependency;
using CodeZero.Domain.Entities;
using CodeZero.Events.Bus;
using CodeZero.Events.Bus.Exceptions;
using CodeZero.Extensions;
using CodeZero.Logging;
using CodeZero.Runtime.Session;
using CodeZero.Runtime.Validation;
using CodeZero.Web.Models;
using CodeZero.WebApi.Configuration;
using CodeZero.WebApi.Controllers;
using Castle.Core.Logging;

namespace CodeZero.WebApi.ExceptionHandling
{
    /// <summary>
    /// Used to handle exceptions on web api controllers.
    /// </summary>
    public class CodeZeroApiExceptionFilterAttribute : ExceptionFilterAttribute, ITransientDependency
    {
        /// <summary>
        /// Reference to the <see cref="ILogger"/>.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Reference to the <see cref="IEventBus"/>.
        /// </summary>
        public IEventBus EventBus { get; set; }

        public ICodeZeroSession CodeZeroSession { get; set; }

        protected ICodeZeroWebApiConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeZeroApiExceptionFilterAttribute"/> class.
        /// </summary>
        public CodeZeroApiExceptionFilterAttribute(ICodeZeroWebApiConfiguration configuration)
        {
            Configuration = configuration;
            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
            CodeZeroSession = NullCodeZeroSession.Instance;
        }

        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var wrapResultAttribute = HttpActionDescriptorHelper
                .GetWrapResultAttributeOrNull(context.ActionContext.ActionDescriptor) ??
                Configuration.DefaultWrapResultAttribute;

            if (wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            if (!wrapResultAttribute.WrapOnError)
            {
                return;
            }

            if (IsIgnoredUrl(context.Request.RequestUri))
            {
                return;
            }

            if (context.Exception is HttpException)
            {
                var httpException = context.Exception as HttpException;
                var httpStatusCode = (HttpStatusCode) httpException.GetHttpCode();

                context.Response = context.Request.CreateResponse(
                    httpStatusCode,
                    new AjaxResponse(
                        new ErrorInfo(httpException.Message),
                        httpStatusCode == HttpStatusCode.Unauthorized || httpStatusCode == HttpStatusCode.Forbidden
                    )
                );
            }
            else
            {
                context.Response = context.Request.CreateResponse(
                    GetStatusCode(context),
                    new AjaxResponse(
                        SingletonDependency<IErrorInfoBuilder>.Instance.BuildForException(context.Exception),
                        context.Exception is CodeZero.Authorization.CodeZeroAuthorizationException)
                );
            }

            EventBus.Trigger(this, new CodeZeroHandledExceptionData(context.Exception));
        }

        protected virtual HttpStatusCode GetStatusCode(HttpActionExecutedContext context)
        {
            if (context.Exception is CodeZero.Authorization.CodeZeroAuthorizationException)
            {
                return CodeZeroSession.UserId.HasValue
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized;
            }

            if (context.Exception is CodeZeroValidationException)
            {
                return HttpStatusCode.BadRequest;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.InternalServerError;
        }

        protected virtual bool IsIgnoredUrl(Uri uri)
        {
            if (uri == null || uri.AbsolutePath.IsNullOrEmpty())
            {
                return false;
            }

            return Configuration.ResultWrappingIgnoreUrls.Any(url => uri.AbsolutePath.StartsWith(url));
        }
    }
}