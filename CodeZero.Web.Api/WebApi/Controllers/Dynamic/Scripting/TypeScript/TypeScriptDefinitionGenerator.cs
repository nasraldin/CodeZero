//  <copyright file="TypeScriptDefinitionGenerator.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using System.Text;
using CodeZero.Dependency;
using CodeZero.Extensions;

namespace CodeZero.WebApi.Controllers.Dynamic.Scripting.TypeScript
{
    public class TypeScriptDefinitionGenerator : ITransientDependency
    {
        private readonly DynamicApiControllerManager _dynamicApiControllerManager;

        public TypeScriptDefinitionGenerator(DynamicApiControllerManager dynamicApiControllerManager)
        {
            _dynamicApiControllerManager = dynamicApiControllerManager;
        }

        public string GetScript()
        {
            var dynamicControllers = _dynamicApiControllerManager.GetAll();
            
            StringBuilder script = new StringBuilder();
            if (dynamicControllers == null || dynamicControllers.Count == 0)
                return "";
            //sorting the controllers and use this sorting for detecting the servicePrefix change
            //we create module per servicePrefix
            var sortedDynamicControllers = dynamicControllers.OrderBy(z => z.ServiceName);
            var servicePrefix = GetServicePrefix(sortedDynamicControllers.First().ServiceName);
            if (servicePrefix.IsNullOrEmpty())
                script.AppendLine("declare module CodeZero.services");//Create a new Module
            else
                script.AppendLine("declare module CodeZero.services." + servicePrefix);//Create a new Module
            script.AppendLine("{");
            var proxyGenerator = new TypeScriptDefinitionProxyGenerator();
            foreach (var dynamicController in sortedDynamicControllers)
            {
                if (servicePrefix != GetServicePrefix(dynamicController.ServiceName))
                {
                    //the service Prefix has been changed
                    servicePrefix = GetServicePrefix(dynamicController.ServiceName);
                    script.AppendLine("}");//Close the Previous Module
                    //Create new module for the new service prefix
                    if(servicePrefix.IsNullOrEmpty())
                        script.AppendLine("declare module CodeZero.services");//Create a new Module
                    else
                        script.AppendLine("declare module CodeZero.services." + servicePrefix);//Create a new Module
                    script.AppendLine("{");
                }
                script.AppendLine(proxyGenerator.Generate(dynamicController,servicePrefix));
                script.AppendLine();
            }
            script.AppendLine("}");
            return script.ToString();
        }

        private string GetServicePrefix(string serviceName)
        {
            if (serviceName.IndexOf('/') == -1)
                return  "";
            else
                return serviceName.Substring(0,serviceName.IndexOf('/'));
        }
    }
}
