//  <copyright file="CodeZeroAspNetCoreConfiguration.cs" project="CodeZero.AspNetCore" solution="CodeZero">
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
    public class CodeZeroAspNetCoreConfiguration : ICodeZeroAspNetCoreConfiguration
    {
        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        public IClientCacheAttribute DefaultClientCacheAttribute { get; set; }

        public UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        public List<Type> FormBodyBindingIgnoredTypes { get; }

        public ControllerAssemblySettingList ControllerAssemblySettings { get; }

        public bool IsValidationEnabledForControllers { get; set; }

        public bool IsAuditingEnabled { get; set; }

        public bool SetNoCacheForAjaxResponses { get; set; }

        public List<Action<IRouteBuilder>> RouteConfiguration { get; }

        public CodeZeroAspNetCoreConfiguration()
        {
            DefaultWrapResultAttribute = new WrapResultAttribute();
            DefaultClientCacheAttribute = new NoClientCacheAttribute(false);
            DefaultUnitOfWorkAttribute = new UnitOfWorkAttribute();
            ControllerAssemblySettings = new ControllerAssemblySettingList();
            FormBodyBindingIgnoredTypes = new List<Type>();
            RouteConfiguration = new List<Action<IRouteBuilder>>();
            IsValidationEnabledForControllers = true;
            SetNoCacheForAjaxResponses = true;
            IsAuditingEnabled = true;
        }
       
        public CodeZeroControllerAssemblySettingBuilder CreateControllersForAppServices(
            Assembly assembly,
            string moduleName = CodeZeroControllerAssemblySetting.DefaultServiceModuleName,
            bool useConventionalHttpVerbs = true)
        {
            var setting = new CodeZeroControllerAssemblySetting(moduleName, assembly, useConventionalHttpVerbs);
            ControllerAssemblySettings.Add(setting);
            return new CodeZeroControllerAssemblySettingBuilder(setting);
        }
    }
}