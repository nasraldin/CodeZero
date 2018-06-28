//  <copyright file="UnitOfWorkMiddleware.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Domain.Uow;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CodeZero.AspNetCore.Uow
{
    public class CodeZeroUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UnitOfWorkMiddlewareOptions _options;

        public CodeZeroUnitOfWorkMiddleware(
            RequestDelegate next, 
            IUnitOfWorkManager unitOfWorkManager, 
            IOptions<UnitOfWorkMiddlewareOptions> options)
        {
            _next = next;
            _unitOfWorkManager = unitOfWorkManager;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!_options.Filter(httpContext))
            {
                await _next(httpContext);
                return;
            }

            using (var uow = _unitOfWorkManager.Begin(_options.OptionsFactory(httpContext)))
            {
                await _next(httpContext);
                await uow.CompleteAsync();
            }
        }
    }
}
