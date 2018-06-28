//  <copyright file="DynamicApiServiceNameHelper.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Text.RegularExpressions;

namespace CodeZero.WebApi.Controllers.Dynamic.Builders
{
    internal static class DynamicApiServiceNameHelper
    {
        private static readonly Regex ServiceNameRegex = new Regex(@"^([a-zA-Z_][a-zA-Z0-9_]*)(\/([a-zA-Z_][a-zA-Z0-9_]*))+$");
        private static readonly Regex ServiceNameWithActionRegex = new Regex(@"^([a-zA-Z_][a-zA-Z0-9_]*)(\/([a-zA-Z_][a-zA-Z0-9_]*)){2,}$");

        public static bool IsValidServiceName(string serviceName)
        {
            return ServiceNameRegex.IsMatch(serviceName);
        }

        public static bool IsValidServiceNameWithAction(string serviceNameWithAction)
        {
            return ServiceNameWithActionRegex.IsMatch(serviceNameWithAction);
        }

        public static string GetServiceNameInServiceNameWithAction(string serviceNameWithAction)
        {
            return serviceNameWithAction.Substring(0, serviceNameWithAction.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase));
        }

        public static string GetActionNameInServiceNameWithAction(string serviceNameWithAction)
        {
            return serviceNameWithAction.Substring(serviceNameWithAction.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase) + 1);
        }
    }
}
