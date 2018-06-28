//  <copyright file="ApiProxyGenerationOptions.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.Collections.Extensions;

namespace CodeZero.Web.Api.ProxyScripting
{
    public class ApiProxyGenerationOptions
    {
        public string GeneratorType { get; set; }

        public bool UseCache { get; set; }

        public string[] Modules { get; set; }

        public string[] Controllers { get; set; }

        public string[] Actions { get; set; }

        public IDictionary<string, string> Properties { get; set; }

        public ApiProxyGenerationOptions(string generatorType, bool useCache = true)
        {
            GeneratorType = generatorType;
            UseCache = useCache;

            Properties = new Dictionary<string, string>();
        }

        public bool IsPartialRequest()
        {
            return !(Modules.IsNullOrEmpty() && Controllers.IsNullOrEmpty() && Actions.IsNullOrEmpty());
        }
    }
}