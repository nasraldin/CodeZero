//  <copyright file="ICodeZeroAspNetCoreConfiguration.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Reflection;
using CodeZero.AspNetCore.Mvc.Results.Caching;
using CodeZero.Domain.Uow;
using CodeZero.Web.Models;
using Microsoft.AspNetCore.Routing;

namespace CodeZero.AspNetCore.Configuration
{
    public interface ICodeZeroAspNetCoreConfiguration
    {
        WrapResultAttribute DefaultWrapResultAttribute { get; }

        IClientCacheAttribute DefaultClientCacheAttribute { get; set; }

        UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        List<Type> FormBodyBindingIgnoredTypes { get; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }

        /// <summary>
        /// Used to enable/disable auditing for MVC controllers.
        /// Default: true.
        /// </summary>
        bool IsAuditingEnabled { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool SetNoCacheForAjaxResponses { get; set; }
        
        /// <summary>
        /// Used to add route config for modules.
        /// </summary>
        List<Action<IRouteBuilder>> RouteConfiguration { get; }

        CodeZeroControllerAssemblySettingBuilder CreateControllersForAppServices(
            Assembly assembly,
            string moduleName = CodeZeroControllerAssemblySetting.DefaultServiceModuleName,
            bool useConventionalHttpVerbs = true
        );
    }
}
