//  <copyright file="CodeZeroValidationActionFilter.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Application.Services;
using CodeZero.Aspects;
using CodeZero.AspNetCore.Configuration;
using CodeZero.AspNetCore.Mvc.Extensions;
using CodeZero.Dependency;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeZero.AspNetCore.Mvc.Validation
{
    public class CodeZeroValidationActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        private readonly ICodeZeroAspNetCoreConfiguration _configuration;

        public CodeZeroValidationActionFilter(IIocResolver iocResolver, ICodeZeroAspNetCoreConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_configuration.IsValidationEnabledForControllers || !context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            using (CodeZeroCrossCuttingConcerns.Applying(context.Controller, CodeZeroCrossCuttingConcerns.Validation))
            {
                using (var validator = _iocResolver.ResolveAsDisposable<MvcActionInvocationValidator>())
                {
                    validator.Object.Initialize(context);
                    validator.Object.Validate();
                }

                await next();
            }
        }
    }
}
