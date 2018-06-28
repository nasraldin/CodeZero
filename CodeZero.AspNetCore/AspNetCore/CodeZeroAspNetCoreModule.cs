//  <copyright file="CodeZeroAspNetCoreModule.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using CodeZero.AspNetCore.Configuration;
using CodeZero.AspNetCore.MultiTenancy;
using CodeZero.AspNetCore.Mvc.Auditing;
using CodeZero.AspNetCore.Runtime.Session;
using CodeZero.AspNetCore.Security.AntiForgery;
using CodeZero.Auditing;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Modules;
using CodeZero.Reflection.Extensions;
using CodeZero.Runtime.Session;
using CodeZero.Web;
using CodeZero.Web.Security.AntiForgery;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Options;

namespace CodeZero.AspNetCore
{
    [DependsOn(typeof(CodeZeroWebCommonModule))]
    public class CodeZeroAspNetCoreModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new CodeZeroAspNetCoreConventionalRegistrar());

            IocManager.Register<ICodeZeroAspNetCoreConfiguration, CodeZeroAspNetCoreConfiguration>();

            Configuration.ReplaceService<IPrincipalAccessor, AspNetCorePrincipalAccessor>(DependencyLifeStyle.Transient);
            Configuration.ReplaceService<ICodeZeroAntiForgeryManager, CodeZeroAspNetCoreAntiForgeryManager>(DependencyLifeStyle.Transient);
            Configuration.ReplaceService<IClientInfoProvider, HttpContextClientInfoProvider>(DependencyLifeStyle.Transient);

            Configuration.Modules.CodeZeroAspNetCore().FormBodyBindingIgnoredTypes.Add(typeof(IFormFile));

            Configuration.MultiTenancy.Resolvers.Add<DomainTenantResolveContributor>();
            Configuration.MultiTenancy.Resolvers.Add<HttpHeaderTenantResolveContributor>();
            Configuration.MultiTenancy.Resolvers.Add<HttpCookieTenantResolveContributor>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroAspNetCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            AddApplicationParts();
            ConfigureAntiforgery();
        }

        private void AddApplicationParts()
        {
            var configuration = IocManager.Resolve<CodeZeroAspNetCoreConfiguration>();
            var partManager = IocManager.Resolve<ApplicationPartManager>();
            var moduleManager = IocManager.Resolve<ICodeZeroModuleManager>();

            var controllerAssemblies = configuration.ControllerAssemblySettings.Select(s => s.Assembly).Distinct();
            foreach (var controllerAssembly in controllerAssemblies)
            {
                partManager.ApplicationParts.Add(new AssemblyPart(controllerAssembly));
            }

            var plugInAssemblies = moduleManager.Modules.Where(m => m.IsLoadedAsPlugIn).Select(m => m.Assembly).Distinct();
            foreach (var plugInAssembly in plugInAssemblies)
            {
                partManager.ApplicationParts.Add(new AssemblyPart(plugInAssembly));
            }
        }

        private void ConfigureAntiforgery()
        {
            IocManager.Using<IOptions<AntiforgeryOptions>>(optionsAccessor =>
            {
                optionsAccessor.Value.HeaderName = Configuration.Modules.CodeZeroWebCommon().AntiForgery.TokenHeaderName;
            });
        }
    }
}