//  <copyright file="JQueryActionScriptGenerator.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Text;
using CodeZero.Extensions;
using CodeZero.Web;
using CodeZero.Web.Api.ProxyScripting.Generators;

namespace CodeZero.WebApi.Controllers.Dynamic.Scripting.jQuery
{
    internal class JQueryActionScriptGenerator
    {
        private readonly DynamicApiControllerInfo _controllerInfo;
        private readonly DynamicApiActionInfo _actionInfo;

        private const string JsMethodTemplate =
@"    serviceNamespace{jsMethodName} = function({jsMethodParameterList}) {
        return CodeZero.ajax($.extend({
{ajaxCallParameters}
        }, ajaxParams));
    };";

        public JQueryActionScriptGenerator(DynamicApiControllerInfo controllerInfo, DynamicApiActionInfo actionInfo)
        {
            _controllerInfo = controllerInfo;
            _actionInfo = actionInfo;
        }

        public virtual string GenerateMethod()
        {
            var jsMethodName = _actionInfo.ActionName.ToCamelCase();
            var jsMethodParameterList = ActionScriptingHelper.GenerateJsMethodParameterList(_actionInfo.Method, "ajaxParams");

            var jsMethod = JsMethodTemplate
                .Replace("{jsMethodName}", ProxyScriptingJsFuncHelper.WrapWithBracketsOrWithDotPrefix(jsMethodName))
                .Replace("{jsMethodParameterList}", jsMethodParameterList)
                .Replace("{ajaxCallParameters}", GenerateAjaxCallParameters());

            return jsMethod;
        }

        protected string GenerateAjaxCallParameters()
        {
            var script = new StringBuilder();
            
            script.AppendLine("            url: CodeZero.appPath + '" + ActionScriptingHelper.GenerateUrlWithParameters(_controllerInfo, _actionInfo) + "',");
            script.AppendLine("            type: '" + _actionInfo.Verb.ToString().ToUpperInvariant() + "',");

            if (_actionInfo.Verb == HttpVerb.Get)
            {
                script.Append("            data: " + ActionScriptingHelper.GenerateBody(_actionInfo));
            }
            else
            {
                script.Append("            data: JSON.stringify(" + ActionScriptingHelper.GenerateBody(_actionInfo) + ")");                
            }
            
            return script.ToString();
        }
    }
}