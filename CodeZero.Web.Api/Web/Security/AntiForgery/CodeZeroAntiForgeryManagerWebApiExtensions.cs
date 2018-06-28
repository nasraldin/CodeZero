//  <copyright file="CodeZeroAntiForgeryManagerWebApiExtensions.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using CodeZero.Extensions;
using CodeZero.WebApi.Extensions;

namespace CodeZero.Web.Security.AntiForgery
{
    public static class CodeZeroAntiForgeryManagerWebApiExtensions
    {
        public static void SetCookie(this ICodeZeroAntiForgeryManager manager, HttpResponseHeaders headers)
        {
            headers.SetCookie(new Cookie(manager.Configuration.TokenCookieName, manager.GenerateToken()));
        }

        public static bool IsValid(this ICodeZeroAntiForgeryManager manager, HttpRequestHeaders headers)
        {
            var cookieTokenValue = GetCookieValue(manager, headers);
            if (cookieTokenValue.IsNullOrEmpty())
            {
                return true;
            }

            var headerTokenValue = GetHeaderValue(manager, headers);
            if (headerTokenValue.IsNullOrEmpty())
            {
                return false;
            }

            return manager.As<ICodeZeroAntiForgeryValidator>().IsValid(cookieTokenValue, headerTokenValue);
        }

        private static string GetCookieValue(ICodeZeroAntiForgeryManager manager, HttpRequestHeaders headers)
        {
            var cookie = headers.GetCookies(manager.Configuration.TokenCookieName).LastOrDefault();
            if (cookie == null)
            {
                return null;
            }

            return cookie[manager.Configuration.TokenCookieName].Value;
        }

        private static string GetHeaderValue(ICodeZeroAntiForgeryManager manager, HttpRequestHeaders headers)
        {
            IEnumerable<string> headerValues;
            if (!headers.TryGetValues(manager.Configuration.TokenHeaderName, out headerValues))
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
