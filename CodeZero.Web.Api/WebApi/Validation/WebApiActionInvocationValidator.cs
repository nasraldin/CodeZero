//  <copyright file="WebApiActionInvocationValidator.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Http.Controllers;
using CodeZero.Collections.Extensions;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Web.Validation;

namespace CodeZero.WebApi.Validation
{
    public class WebApiActionInvocationValidator : ActionInvocationValidatorBase
    {
        protected HttpActionContext ActionContext { get; private set; }

        public WebApiActionInvocationValidator(IValidationConfiguration configuration, IIocResolver iocResolver)
            : base(configuration, iocResolver)
        {
        }

        public void Initialize(HttpActionContext actionContext, MethodInfo methodInfo)
        {
            ActionContext = actionContext;

            base.Initialize(methodInfo);
        }

        protected override void SetDataAnnotationAttributeErrors()
        {
            foreach (var state in ActionContext.ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    ValidationErrors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
                }
            }
        }

        protected override object GetParameterValue(string parameterName)
        {
            return ActionContext.ActionArguments.GetOrDefault(parameterName);
        }
    }
}