//  <copyright file="CodeZeroServiceProxiesController.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.AspNetCore.Mvc.Controllers;
using CodeZero.Auditing;
using CodeZero.Web.Api.ProxyScripting;
using CodeZero.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeZero.AspNetCore.Mvc.Proxying
{
    [DontWrapResult]
    [DisableAuditing]
    public class CodeZeroServiceProxiesController : CodeZeroController
    {
        private readonly IApiProxyScriptManager _proxyScriptManager;

        public CodeZeroServiceProxiesController(IApiProxyScriptManager proxyScriptManager)
        {
            _proxyScriptManager = proxyScriptManager;
        }

        [Produces("application/x-javascript")]
        public ContentResult GetAll(ApiProxyGenerationModel model)
        {
            var script = _proxyScriptManager.GetScript(model.CreateOptions());
            return Content(script, "application/x-javascript");
        }
    }
}
