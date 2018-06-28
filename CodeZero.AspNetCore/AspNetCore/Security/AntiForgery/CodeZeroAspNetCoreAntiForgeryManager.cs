//  <copyright file="CodeZeroAspNetCoreAntiForgeryManager.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Web.Security.AntiForgery;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace CodeZero.AspNetCore.Security.AntiForgery
{
    public class CodeZeroAspNetCoreAntiForgeryManager : ICodeZeroAntiForgeryManager
    {
        public ICodeZeroAntiForgeryConfiguration Configuration { get; }

        private readonly IAntiforgery _antiforgery;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CodeZeroAspNetCoreAntiForgeryManager(
            IAntiforgery antiforgery,
            IHttpContextAccessor httpContextAccessor,
            ICodeZeroAntiForgeryConfiguration configuration)
        {
            Configuration = configuration;
            _antiforgery = antiforgery;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateToken()
        {
            return _antiforgery.GetAndStoreTokens(_httpContextAccessor.HttpContext).RequestToken;
        }
    }
}