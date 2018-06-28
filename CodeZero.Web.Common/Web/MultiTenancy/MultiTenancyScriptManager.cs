//  <copyright file="MultiTenancyScriptManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Globalization;
using System.Text;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Extensions;
using CodeZero.MultiTenancy;

namespace CodeZero.Web.MultiTenancy
{
    public class MultiTenancyScriptManager : IMultiTenancyScriptManager, ITransientDependency
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        public MultiTenancyScriptManager(IMultiTenancyConfig multiTenancyConfig)
        {
            _multiTenancyConfig = multiTenancyConfig;
        }

        public string GetScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(CodeZero){");
            script.AppendLine();

            script.AppendLine("    CodeZero.multiTenancy = CodeZero.multiTenancy || {};");
            script.AppendLine("    CodeZero.multiTenancy.isEnabled = " + _multiTenancyConfig.IsEnabled.ToString().ToLowerInvariant() + ";");

            script.AppendLine();
            script.Append("})(CodeZero);");

            return script.ToString();
        }
    }
}