//  <copyright file="CodeZeroObjectActionResultWrapper.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Buffers;
using System.Linq;

namespace CodeZero.AspNetCore.Mvc.Results.Wrapping
{
    public class CodeZeroObjectActionResultWrapper : ICodeZeroActionResultWrapper
    {
        private readonly IServiceProvider _serviceProvider;

        public CodeZeroObjectActionResultWrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Wrap(ResultExecutingContext actionResult)
        {
            //var objectResult = actionResult.Result as ObjectResult;
            if (!(actionResult.Result is ObjectResult objectResult))
            {
                throw new ArgumentException($"{nameof(actionResult)} should be ObjectResult!");
            }

            if (!(objectResult.Value is AjaxResponseBase))
            {
                objectResult.Value = new AjaxResponse(objectResult.Value);
                if (!objectResult.Formatters.Any(f => f is JsonOutputFormatter))
                {
                    objectResult.Formatters.Add(
                        new JsonOutputFormatter(
                            _serviceProvider.GetRequiredService<IOptions<MvcJsonOptions>>().Value.SerializerSettings,
                            _serviceProvider.GetRequiredService<ArrayPool<char>>()
                        )
                    );
                }
            }
        }
    }
}