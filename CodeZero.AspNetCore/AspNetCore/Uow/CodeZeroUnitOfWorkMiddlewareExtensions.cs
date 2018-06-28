//  <copyright file="CodeZeroUnitOfWorkMiddlewareExtensions.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.AspNetCore.Uow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class CodeZeroUnitOfWorkMiddlewareExtensions
    {
        public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder app, Action<UnitOfWorkMiddlewareOptions> optionsAction = null)
        {
            var options = app.ApplicationServices.GetRequiredService<IOptions<UnitOfWorkMiddlewareOptions>>().Value;
            optionsAction?.Invoke(options);
            return app.UseMiddleware<CodeZeroUnitOfWorkMiddleware>();
        }
    }
}
