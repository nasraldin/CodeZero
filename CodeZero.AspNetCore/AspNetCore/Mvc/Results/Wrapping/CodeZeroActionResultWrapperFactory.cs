//  <copyright file="CodeZeroActionResultWrapperFactory.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeZero.AspNetCore.Mvc.Results.Wrapping
{
    public class CodeZeroActionResultWrapperFactory : ICodeZeroActionResultWrapperFactory
    {
        public ICodeZeroActionResultWrapper CreateFor(ResultExecutingContext actionResult)
        {
            Check.NotNull(actionResult, nameof(actionResult));

            if (actionResult.Result is ObjectResult)
            {
                return new CodeZeroObjectActionResultWrapper(actionResult.HttpContext.RequestServices);
            }

            if (actionResult.Result is JsonResult)
            {
                return new CodeZeroJsonActionResultWrapper();
            }

            if (actionResult.Result is EmptyResult)
            {
                return new CodeZeroEmptyActionResultWrapper();
            }

            return new NullCodeZeroActionResultWrapper();
        }
    }
}