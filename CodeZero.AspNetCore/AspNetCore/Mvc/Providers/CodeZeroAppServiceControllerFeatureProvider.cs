//  <copyright file="CodeZeroAppServiceControllerFeatureProvider.cs" project="CodeZero.AspNetCore" solution="CodeZero">
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
using CodeZero.Dependency;
using CodeZero.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CodeZero.AspNetCore.Mvc.Providers
{
    /// <summary>
    /// Used to add application services as controller.
    /// </summary>
    public class CodeZeroAppServiceControllerFeatureProvider : ControllerFeatureProvider
    {
        private readonly IIocResolver _iocResolver;

        public CodeZeroAppServiceControllerFeatureProvider(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            var type = typeInfo.AsType();

            if (!typeof(IApplicationService).IsAssignableFrom(type) ||
                !typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType)
            {
                return false;
            }

            var remoteServiceAttr = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(typeInfo);

            if (remoteServiceAttr != null && !remoteServiceAttr.IsEnabledFor(type))
            {
                return false;
            }

            var configuration = _iocResolver.Resolve<CodeZeroAspNetCoreConfiguration>().ControllerAssemblySettings.GetSettingOrNull(type);
            return configuration != null && configuration.TypePredicate(type);
        }
    }
}