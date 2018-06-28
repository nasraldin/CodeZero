//  <copyright file="MvcActionInvocationValidator.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.ComponentModel.DataAnnotations;
using CodeZero.AspNetCore.Mvc.Extensions;
using CodeZero.Collections.Extensions;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Web.Validation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeZero.AspNetCore.Mvc.Validation
{
    public class MvcActionInvocationValidator : ActionInvocationValidatorBase
    {
        protected ActionExecutingContext ActionContext { get; private set; }

        public MvcActionInvocationValidator(IValidationConfiguration configuration, IIocResolver iocResolver)
            : base(configuration, iocResolver)
        {
        }

        public void Initialize(ActionExecutingContext actionContext)
        {
            ActionContext = actionContext;

            base.Initialize(actionContext.ActionDescriptor.GetMethodInfo());
        }

        protected override object GetParameterValue(string parameterName)
        {
            return ActionContext.ActionArguments.GetOrDefault(parameterName);
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
    }
}