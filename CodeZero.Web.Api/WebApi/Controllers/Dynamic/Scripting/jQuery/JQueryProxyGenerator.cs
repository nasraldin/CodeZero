//  <copyright file="JQueryProxyGenerator.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Text;
using CodeZero.Extensions;
using CodeZero.Web.Api.ProxyScripting.Generators;

namespace CodeZero.WebApi.Controllers.Dynamic.Scripting.jQuery
{
    internal class JQueryProxyGenerator : IScriptProxyGenerator
    {
        private readonly DynamicApiControllerInfo _controllerInfo;
        private readonly bool _defineAmdModule;

        public JQueryProxyGenerator(DynamicApiControllerInfo controllerInfo, bool defineAmdModule = true)
        {
            _controllerInfo = controllerInfo;
            _defineAmdModule = defineAmdModule;
        }

        public string Generate()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine();
            script.AppendLine("    var serviceNamespace = CodeZero.utils.createNamespace(CodeZero, 'services." + _controllerInfo.ServiceName.Replace("/", ".") + "');");
            script.AppendLine();

            //generate all methods
            foreach (var methodInfo in _controllerInfo.Actions.Values)
            {
                AppendMethod(script, _controllerInfo, methodInfo);
                script.AppendLine();
            }

            //generate amd module definition
            if (_defineAmdModule)
            {
                script.AppendLine("    if(typeof define === 'function' && define.amd){");
                script.AppendLine("        define(function (require, exports, module) {");
                script.AppendLine("            return {");

                var methodNo = 0;
                foreach (var methodInfo in _controllerInfo.Actions.Values)
                {
                    script.AppendLine("                '" + methodInfo.ActionName.ToCamelCase() + "' : serviceNamespace" + ProxyScriptingJsFuncHelper.WrapWithBracketsOrWithDotPrefix(methodInfo.ActionName.ToCamelCase()) + ((methodNo++) < (_controllerInfo.Actions.Count - 1) ? "," : ""));
                }

                script.AppendLine("            };");
                script.AppendLine("        });");
                script.AppendLine("    }");
            }

            script.AppendLine();
            script.AppendLine("})();");

            return script.ToString();
        }

        private static void AppendMethod(StringBuilder script, DynamicApiControllerInfo controllerInfo, DynamicApiActionInfo methodInfo)
        {
            var generator = new JQueryActionScriptGenerator(controllerInfo, methodInfo);
            script.AppendLine(generator.GenerateMethod());
        }
    }
}