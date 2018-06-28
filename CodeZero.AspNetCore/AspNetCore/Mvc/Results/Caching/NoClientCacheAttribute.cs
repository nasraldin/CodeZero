//  <copyright file="NoClientCacheAttribute.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeZero.AspNetCore.Mvc.Results.Caching
{
    public class NoClientCacheAttribute : IClientCacheAttribute
    {
        /// <summary>
        /// Default: false.
        /// </summary>
        public bool IncludeNonAjaxRequests { get; set; }

        public NoClientCacheAttribute()
            : this(false)
        {
            
        }

        public NoClientCacheAttribute(bool includeNonAjaxRequests)
        {
            IncludeNonAjaxRequests = includeNonAjaxRequests;
        }

        public virtual void Apply(ResultExecutingContext context)
        {
            if (IncludeNonAjaxRequests || context.HttpContext.Request.IsAjaxRequest())
            {
                context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate, max-age=0";
                context.HttpContext.Response.Headers["Pragma"] = "no-cache";
                context.HttpContext.Response.Headers["Expires"] = "0";
            }
        }
    }
}
