//  <copyright file="AspNetCoreApiDescriptionModelProvider.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using System.Reflection;
using CodeZero.Application.Services;
using CodeZero.AspNetCore.Configuration;
using CodeZero.AspNetCore.Mvc.Extensions;
using CodeZero.AspNetCore.Mvc.Proxying.Utils;
using CodeZero.Dependency;
using CodeZero.Extensions;
using CodeZero.Reflection.Extensions;
using CodeZero.Threading;
using CodeZero.Web.Api.Modeling;
using CodeZero.Web.Api.ProxyScripting.Configuration;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CodeZero.AspNetCore.Mvc.Proxying
{
    public class AspNetCoreApiDescriptionModelProvider : IApiDescriptionModelProvider, ISingletonDependency
    {
        public ILogger Logger { get; set; }

        private readonly IApiDescriptionGroupCollectionProvider _descriptionProvider;
        private readonly CodeZeroAspNetCoreConfiguration _configuration;
        private readonly IApiProxyScriptingConfiguration _apiProxyScriptingConfiguration;

        public AspNetCoreApiDescriptionModelProvider(
            IApiDescriptionGroupCollectionProvider descriptionProvider,
            CodeZeroAspNetCoreConfiguration configuration,
            IApiProxyScriptingConfiguration apiProxyScriptingConfiguration)
        {
            _descriptionProvider = descriptionProvider;
            _configuration = configuration;
            _apiProxyScriptingConfiguration = apiProxyScriptingConfiguration;

            Logger = NullLogger.Instance;
        }

        public ApplicationApiDescriptionModel CreateModel()
        {
            var model = new ApplicationApiDescriptionModel();

            foreach (var descriptionGroupItem in _descriptionProvider.ApiDescriptionGroups.Items)
            {
                foreach (var apiDescription in descriptionGroupItem.Items)
                {
                    if (!apiDescription.ActionDescriptor.IsControllerAction())
                    {
                        continue;
                    }

                    AddApiDescriptionToModel(apiDescription, model);
                }
            }

            return model;
        }

        private void AddApiDescriptionToModel(ApiDescription apiDescription, ApplicationApiDescriptionModel model)
        {
            var moduleModel = model.GetOrAddModule(GetModuleName(apiDescription));
            var controllerModel = moduleModel.GetOrAddController(GetControllerName(apiDescription));

            var method = apiDescription.ActionDescriptor.GetMethodInfo();
            var methodName = GetNormalizedMethodName(controllerModel, method);

            if (controllerModel.Actions.ContainsKey(methodName))
            {
                Logger.Warn($"Controller '{controllerModel.Name}' contains more than one action with name '{methodName}' for module '{moduleModel.Name}'. Ignored: " + apiDescription.ActionDescriptor.GetMethodInfo());
                return;
            }

            var returnValue = new ReturnValueApiDescriptionModel(method.ReturnType);

            var actionModel = controllerModel.AddAction(new ActionApiDescriptionModel(
                methodName,
                returnValue,
                apiDescription.RelativePath,
                apiDescription.HttpMethod
            ));

            AddParameterDescriptionsToModel(actionModel, method, apiDescription);
        }

        private string GetNormalizedMethodName(ControllerApiDescriptionModel controllerModel, MethodInfo method)
        {
            if (!_apiProxyScriptingConfiguration.RemoveAsyncPostfixOnProxyGeneration)
            {
                return method.Name;
            }

            if (!method.IsAsync())
            {
                return method.Name;
            }

            var normalizedName = method.Name.RemovePostFix("Async");
            if (controllerModel.Actions.ContainsKey(normalizedName))
            {
                return method.Name;
            }

            return normalizedName;
        }

        private static string GetControllerName(ApiDescription apiDescription)
        {
            return apiDescription.GroupName?.RemovePostFix(ApplicationService.CommonPostfixes) 
                   ?? apiDescription.ActionDescriptor.AsControllerActionDescriptor().ControllerName;
        }

        private void AddParameterDescriptionsToModel(ActionApiDescriptionModel actionModel, MethodInfo method, ApiDescription apiDescription)
        {
            if (!apiDescription.ParameterDescriptions.Any())
            {
                return;
            }

            var matchedMethodParamNames = ArrayMatcher.Match(
                apiDescription.ParameterDescriptions.Select(p => p.Name).ToArray(),
                method.GetParameters().Select(GetMethodParamName).ToArray()
            );

            for (var i = 0; i < apiDescription.ParameterDescriptions.Count; i++)
            {
                var parameterDescription = apiDescription.ParameterDescriptions[i];
                var matchedMethodParamName = matchedMethodParamNames.Length > i
                                                 ? matchedMethodParamNames[i]
                                                 : parameterDescription.Name;

                actionModel.AddParameter(new ParameterApiDescriptionModel(
                        parameterDescription.Name,
                        matchedMethodParamName,
                        parameterDescription.Type,
                        parameterDescription.RouteInfo?.IsOptional ?? false,
                        parameterDescription.RouteInfo?.DefaultValue,
                        parameterDescription.RouteInfo?.Constraints?.Select(c => c.GetType().Name).ToArray(),
                        parameterDescription.Source.Id
                    )
                );
            }
        }

        public string GetMethodParamName(ParameterInfo parameterInfo)
        {
            var modelNameProvider = parameterInfo.GetCustomAttributes()
                .OfType<IModelNameProvider>()
                .FirstOrDefault();

            if (modelNameProvider == null)
            {
                return parameterInfo.Name;
            }

            return modelNameProvider.Name;
        }

        private string GetModuleName(ApiDescription apiDescription)
        {
            var controllerType = apiDescription.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType();
            if (controllerType == null)
            {
                return CodeZeroControllerAssemblySetting.DefaultServiceModuleName;
            }

            foreach (var controllerSetting in _configuration.ControllerAssemblySettings)
            {
                if (Equals(controllerType.GetAssembly(), controllerSetting.Assembly))
                {
                    return controllerSetting.ModuleName;
                }
            }

            return CodeZeroControllerAssemblySetting.DefaultServiceModuleName;
        }
    }
}
