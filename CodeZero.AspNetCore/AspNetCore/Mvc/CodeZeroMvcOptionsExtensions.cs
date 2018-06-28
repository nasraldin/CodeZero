//  <copyright file="CodeZeroMvcOptionsExtensions.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.AspNetCore.Mvc.Auditing;
using CodeZero.AspNetCore.Mvc.Authorization;
using CodeZero.AspNetCore.Mvc.Conventions;
using CodeZero.AspNetCore.Mvc.ExceptionHandling;
using CodeZero.AspNetCore.Mvc.ModelBinding;
using CodeZero.AspNetCore.Mvc.Results;
using CodeZero.AspNetCore.Mvc.Uow;
using CodeZero.AspNetCore.Mvc.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CodeZero.AspNetCore.Mvc
{
    internal static class CodeZeroMvcOptionsExtensions
    {
        public static void AddCodeZero(this MvcOptions options, IServiceCollection services)
        {
            AddConventions(options, services);
            AddFilters(options);
            AddModelBinders(options);
        }

        private static void AddConventions(MvcOptions options, IServiceCollection services)
        {
            options.Conventions.Add(new CodeZeroAppServiceConvention(services));
        }

        private static void AddFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(CodeZeroAuthorizationFilter));
            options.Filters.AddService(typeof(CodeZeroAuditActionFilter));
            options.Filters.AddService(typeof(CodeZeroValidationActionFilter));
            options.Filters.AddService(typeof(CodeZeroUowActionFilter));
            options.Filters.AddService(typeof(CodeZeroExceptionFilter));
            options.Filters.AddService(typeof(CodeZeroResultFilter));
        }

        private static void AddModelBinders(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new CodeZeroDateTimeModelBinderProvider());
        }
    }
}