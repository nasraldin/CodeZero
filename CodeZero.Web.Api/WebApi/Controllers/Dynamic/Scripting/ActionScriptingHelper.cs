//  <copyright file="ActionScriptingHelper.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using System.Reflection;
using CodeZero.Collections.Extensions;
using CodeZero.Extensions;
using CodeZero.Reflection;

namespace CodeZero.WebApi.Controllers.Dynamic.Scripting
{
    internal static class ActionScriptingHelper
    {
        public static string GenerateUrlWithParameters(DynamicApiControllerInfo controllerInfo, DynamicApiActionInfo actionInfo)
        {
            var baseUrl = "api/services/" + controllerInfo.ServiceName + "/" + actionInfo.ActionName;

            var primitiveParameters = actionInfo.Method
                .GetParameters()
                .Where(p => TypeHelper.IsPrimitiveExtendedIncludingNullable(p.ParameterType))
                .ToArray();

            if (!primitiveParameters.Any())
            {
                return baseUrl;
            }

            var qsBuilderParams = primitiveParameters
                .Select(p => $"{{ name: '{p.Name.ToCamelCase()}', value: " + p.Name.ToCamelCase() + " }")
                .JoinAsString(", ");

            return baseUrl + $"' + CodeZero.utils.buildQueryString([{qsBuilderParams}]) + '";
        }

        public static string GenerateJsMethodParameterList(MethodInfo methodInfo, string ajaxParametersName)
        {
            var paramNames = methodInfo.GetParameters().Select(prm => prm.Name.ToCamelCase()).ToList();
            paramNames.Add(ajaxParametersName);
            return string.Join(", ", paramNames);
        }

        public static string GenerateBody(DynamicApiActionInfo actionInfo)
        {
            var parameters = actionInfo.Method
                .GetParameters()
                .Where(p => !TypeHelper.IsPrimitiveExtendedIncludingNullable(p.ParameterType))
                .ToArray();

            if (parameters.Length <= 0)
            {
                return "{}";
            }

            if (parameters.Length > 1)
            {
                throw new CodeZeroException("Only one complex type allowed as argument to a web api controller action. But " + actionInfo.ActionName + " contains more than one!");
            }

            return parameters[0].Name.ToCamelCase();
        }
    }
}
