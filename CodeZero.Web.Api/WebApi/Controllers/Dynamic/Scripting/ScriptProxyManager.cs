//  <copyright file="ScriptProxyManager.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using CodeZero.Collections.Extensions;
using CodeZero.Dependency;
using CodeZero.WebApi.Controllers.Dynamic.Scripting.Angular;
using CodeZero.WebApi.Controllers.Dynamic.Scripting.jQuery;

namespace CodeZero.WebApi.Controllers.Dynamic.Scripting
{
    //TODO@Halil: This class can be optimized.
    public class ScriptProxyManager : ISingletonDependency
    {
        private readonly IDictionary<string, ScriptInfo> CachedScripts;
        private readonly DynamicApiControllerManager _dynamicApiControllerManager;

        public ScriptProxyManager(DynamicApiControllerManager dynamicApiControllerManager)
        {
            _dynamicApiControllerManager = dynamicApiControllerManager;
            CachedScripts = new Dictionary<string, ScriptInfo>();
        }

        public string GetScript(string name, ProxyScriptType type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is null or empty!", nameof(name));
            }

            var cacheKey = type + "_" + name;

            lock (CachedScripts)
            {
                var cachedScript = CachedScripts.GetOrDefault(cacheKey);
                if (cachedScript == null)
                {
                    var dynamicController = _dynamicApiControllerManager
                        .GetAll()
                        .FirstOrDefault(ci => ci.ServiceName == name && ci.IsProxyScriptingEnabled);

                    if (dynamicController == null)
                    {
                        throw new HttpException((int)HttpStatusCode.NotFound, "There is no such a service: " + cacheKey);
                    }

                    var script = CreateProxyGenerator(type, dynamicController, true).Generate();
                    CachedScripts[cacheKey] = cachedScript = new ScriptInfo(script);
                }

                return cachedScript.Script;
            }
        }

        public string GetAllScript(ProxyScriptType type)
        {
            lock (CachedScripts)
            {
                var cacheKey = type + "_all";
                if (!CachedScripts.ContainsKey(cacheKey))
                {
                    var script = new StringBuilder();

                    var dynamicControllers = _dynamicApiControllerManager.GetAll().Where(ci => ci.IsProxyScriptingEnabled);
                    foreach (var dynamicController in dynamicControllers)
                    {
                        var proxyGenerator = CreateProxyGenerator(type, dynamicController, false);
                        script.AppendLine(proxyGenerator.Generate());
                        script.AppendLine();
                    }

                    CachedScripts[cacheKey] = new ScriptInfo(script.ToString());
                }

                return CachedScripts[cacheKey].Script;
            }
        }

        private static IScriptProxyGenerator CreateProxyGenerator(ProxyScriptType type, DynamicApiControllerInfo controllerInfo, bool amdModule)
        {
            switch (type)
            {
                case ProxyScriptType.JQuery:
                    return new JQueryProxyGenerator(controllerInfo, amdModule);
                case ProxyScriptType.Angular:
                    return new AngularProxyGenerator(controllerInfo);
                default:
                    throw new CodeZeroException("Unknown ProxyScriptType: " + type);
            }
        }

        private class ScriptInfo
        {
            public string Script { get; private set; }

            public ScriptInfo(string script)
            {
                Script = script;
            }
        }
    }
}
