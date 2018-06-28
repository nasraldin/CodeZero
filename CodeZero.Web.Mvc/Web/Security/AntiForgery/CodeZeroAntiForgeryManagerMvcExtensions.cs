//  <copyright file="CodeZeroAntiForgeryManagerMvcExtensions.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using CodeZero.Extensions;

namespace CodeZero.Web.Security.AntiForgery
{
    public static class CodeZeroAntiForgeryManagerMvcExtensions
    {
        public static void SetCookie(this ICodeZeroAntiForgeryManager manager, HttpContextBase context, IIdentity identity = null)
        {
            if (identity != null)
            {
                context.User = new ClaimsPrincipal(identity);
            }

            context.Response.Cookies.Add(new HttpCookie(manager.Configuration.TokenCookieName, manager.GenerateToken()));
        }

        public static bool IsValid(this ICodeZeroAntiForgeryManager manager, HttpContextBase context)
        {
            var cookieValue = GetCookieValue(context);
            if (cookieValue.IsNullOrEmpty())
            {
                return true;
            }

            var formOrHeaderValue = manager.Configuration.GetFormOrHeaderValue(context);
            if (formOrHeaderValue.IsNullOrEmpty())
            {
                return false;
            }

            return manager.As<ICodeZeroAntiForgeryValidator>().IsValid(cookieValue, formOrHeaderValue);
        }

        private static string GetCookieValue(HttpContextBase context)
        {
            var cookie = context.Request.Cookies[AntiForgeryConfig.CookieName];
            return cookie?.Value;
        }

        private static string GetFormOrHeaderValue(this ICodeZeroAntiForgeryConfiguration configuration, HttpContextBase context)
        {
            var formValue = context.Request.Form["__RequestVerificationToken"];
            if (!formValue.IsNullOrEmpty())
            {
                return formValue;
            }

            var headerValues = context.Request.Headers.GetValues(configuration.TokenHeaderName);
            if (headerValues == null)
            {
                return null;
            }

            var headersArray = headerValues.ToArray();
            if (!headersArray.Any())
            {
                return null;
            }

            return headersArray.Last().Split(", ").Last();
        }
    }
}