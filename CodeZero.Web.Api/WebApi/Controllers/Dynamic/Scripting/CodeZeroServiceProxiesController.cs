//  <copyright file="CodeZeroServiceProxiesController.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Net.Http;
using System.Net.Http.Headers;
using CodeZero.Auditing;
using CodeZero.Web.Models;
using CodeZero.Web.Security.AntiForgery;
using CodeZero.WebApi.Controllers.Dynamic.Formatters;

namespace CodeZero.WebApi.Controllers.Dynamic.Scripting
{
    /// <summary>
    /// This class is used to create proxies to call dynamic api methods from JavaScript clients.
    /// </summary>
    [DontWrapResult]
    [DisableAuditing]
    [DisableCodeZeroAntiForgeryTokenValidation]
    public class CodeZeroServiceProxiesController : CodeZeroApiController
    {
        private readonly ScriptProxyManager _scriptProxyManager;

        public CodeZeroServiceProxiesController(ScriptProxyManager scriptProxyManager)
        {
            _scriptProxyManager = scriptProxyManager;
        }

        /// <summary>
        /// Gets JavaScript proxy for given service name.
        /// </summary>
        /// <param name="name">Name of the service</param>
        /// <param name="type">Script type</param>
        public HttpResponseMessage Get(string name, ProxyScriptType type = ProxyScriptType.JQuery)
        {
            var script = _scriptProxyManager.GetScript(name, type);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }

        /// <summary>
        /// Gets JavaScript proxy for all services.
        /// </summary>
        /// <param name="type">Script type</param>
        public HttpResponseMessage GetAll(ProxyScriptType type = ProxyScriptType.JQuery)
        {
            var script = _scriptProxyManager.GetAllScript(type);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }
    }
}