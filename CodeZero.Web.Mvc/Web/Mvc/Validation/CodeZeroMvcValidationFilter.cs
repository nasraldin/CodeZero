//  <copyright file="CodeZeroMvcValidationFilter.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Web.Mvc;
using CodeZero.Dependency;
using CodeZero.Web.Mvc.Configuration;
using CodeZero.Web.Mvc.Extensions;

namespace CodeZero.Web.Mvc.Validation
{
    public class CodeZeroMvcValidationFilter : IActionFilter, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        private readonly ICodeZeroMvcConfiguration _configuration;

        public CodeZeroMvcValidationFilter(IIocResolver iocResolver, ICodeZeroMvcConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!_configuration.IsValidationEnabledForControllers)
            {
                return;
            }

            var methodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return;
            }

            using (var validator = _iocResolver.ResolveAsDisposable<MvcActionInvocationValidator>())
            {
                validator.Object.Initialize(filterContext, methodInfo);
                validator.Object.Validate();
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}
