//  <copyright file="CodeZeroWebMvcModule.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using CodeZero.Configuration.Startup;
using CodeZero.Modules;
using CodeZero.Web.Mvc.Auditing;
using CodeZero.Web.Mvc.Authorization;
using CodeZero.Web.Mvc.Configuration;
using CodeZero.Web.Mvc.Controllers;
using CodeZero.Web.Mvc.ModelBinding.Binders;
using CodeZero.Web.Mvc.Resources.Embedded;
using CodeZero.Web.Mvc.Security.AntiForgery;
using CodeZero.Web.Mvc.Uow;
using CodeZero.Web.Mvc.Validation;
using CodeZero.Web.Security.AntiForgery;

namespace CodeZero.Web.Mvc
{
    /// <summary>
    /// This module is used to build ASP.NET MVC web sites using CodeZero.
    /// </summary>
    [DependsOn(typeof(CodeZeroWebModule))]
    public class CodeZeroWebMvcModule : CodeZeroModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());

            IocManager.Register<ICodeZeroMvcConfiguration, CodeZeroMvcConfiguration>();

            Configuration.ReplaceService<ICodeZeroAntiForgeryManager, CodeZeroMvcAntiForgeryManager>();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager));
            HostingEnvironment.RegisterVirtualPathProvider(IocManager.Resolve<EmbeddedResourceVirtualPathProvider>());
        }

        /// <inheritdoc/>
        public override void PostInitialize()
        {
            GlobalFilters.Filters.Add(IocManager.Resolve<CodeZeroMvcAuthorizeFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<CodeZeroAntiForgeryMvcFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<CodeZeroMvcAuditFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<CodeZeroMvcValidationFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<CodeZeroMvcUowFilter>());

            var CodeZeroMvcDateTimeBinder = new CodeZeroMvcDateTimeBinder();
            ModelBinders.Binders.Add(typeof(DateTime), CodeZeroMvcDateTimeBinder);
            ModelBinders.Binders.Add(typeof(DateTime?), CodeZeroMvcDateTimeBinder);
        }
    }
}
