//  <copyright file="TypeScriptController.cs" project="CodeZero.Web.Api" solution="CodeZero">
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
using CodeZero.Web.Security;
using CodeZero.Web.Security.AntiForgery;
using CodeZero.WebApi.Controllers.Dynamic.Formatters;
using CodeZero.WebApi.Controllers.Dynamic.Scripting.TypeScript;

namespace CodeZero.WebApi.Controllers.Dynamic.Scripting
{
    [DontWrapResult]
    [DisableAuditing]
    [DisableCodeZeroAntiForgeryTokenValidation]
    public class TypeScriptController : CodeZeroApiController
    {
        readonly TypeScriptDefinitionGenerator _typeScriptDefinitionGenerator;
        readonly TypeScriptServiceGenerator _typeScriptServiceGenerator;
        public TypeScriptController(TypeScriptDefinitionGenerator typeScriptDefinitionGenerator, TypeScriptServiceGenerator typeScriptServiceGenerator)
        {
            _typeScriptDefinitionGenerator = typeScriptDefinitionGenerator;
            _typeScriptServiceGenerator = typeScriptServiceGenerator;
        }
        
        public HttpResponseMessage Get(bool isCompleteService = false)
        {
            if (isCompleteService)
            {
                var script = _typeScriptServiceGenerator.GetScript();
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
                return response;
            }
            else
            {
                var script = _typeScriptDefinitionGenerator.GetScript();
                var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
                return response;
            }
        }
    }
}
