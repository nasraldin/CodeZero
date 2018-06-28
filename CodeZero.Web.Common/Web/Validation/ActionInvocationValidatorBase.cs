//  <copyright file="ActionInvocationValidatorBase.cs" project="CodeZero.Web.Common" solution="CodeZero">
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
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Runtime.Validation.Interception;

namespace CodeZero.Web.Validation
{
    public abstract class ActionInvocationValidatorBase : MethodInvocationValidator
    {
        protected IList<Type> ValidatorsToSkip => new List<Type>
        {
            typeof(DataAnnotationsValidator),
            typeof(ValidatableObjectValidator)
        };

        protected ActionInvocationValidatorBase(IValidationConfiguration configuration, IIocResolver iocResolver)
            : base(configuration, iocResolver)
        {
        }

        public void Initialize(MethodInfo method)
        {
            base.Initialize(
                method,
                GetParameterValues(method)
            );
        }

        protected override bool ShouldValidateUsingValidator(object validatingObject, Type validatorType)
        {
            // Skip DataAnnotations and IValidatableObject validation because MVC does this automatically
            if (ValidatorsToSkip.Contains(validatorType))
            {
                return false;
            }

            return base.ShouldValidateUsingValidator(validatingObject, validatorType);
        }

        protected override void ValidateMethodParameter(ParameterInfo parameterInfo, object parameterValue)
        {
            // If action parameter value is null then set only ModelState errors
            if (parameterValue != null)
            {
                base.ValidateMethodParameter(parameterInfo, parameterValue);
            }

            SetDataAnnotationAttributeErrors();
        }

        protected virtual object[] GetParameterValues(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var parameterValues = new object[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                parameterValues[i] = GetParameterValue(parameters[i].Name);
            }

            return parameterValues;
        }

        protected abstract void SetDataAnnotationAttributeErrors();

        protected abstract object GetParameterValue(string parameterName);
    }
}
