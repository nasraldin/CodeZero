//  <copyright file="CodeZeroAntiForgeryManagerAspNetCoreExtensions.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace CodeZero.Web.Security.AntiForgery
{
    public static class CodeZeroAntiForgeryManagerAspNetCoreExtensions
    {
        public static void SetCookie(this ICodeZeroAntiForgeryManager manager, HttpContext context, IIdentity identity = null)
        {
            if (identity != null)
            {
                context.User = new ClaimsPrincipal(identity);
            }

            context.Response.Cookies.Append(manager.Configuration.TokenCookieName, manager.GenerateToken());
        }
    }
}
