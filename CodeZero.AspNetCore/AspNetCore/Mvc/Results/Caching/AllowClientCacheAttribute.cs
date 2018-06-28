//  <copyright file="AllowClientCacheAttribute.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeZero.AspNetCore.Mvc.Results.Caching
{
    public class AllowClientCacheAttribute : IClientCacheAttribute
    {
        public ClientCacheScope? Scope { get; }

        public AllowClientCacheAttribute()
        {
            
        }

        public AllowClientCacheAttribute(ClientCacheScope scope)
        {
            Scope = scope;
        }

        public void Apply(ResultExecutingContext context)
        {
            if (Scope.HasValue)
            {
                context.HttpContext.Response.Headers["Cache-Control"] = Scope.ToString().ToCamelCase();
            }
        }
    }
}