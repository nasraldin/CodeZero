//  <copyright file="CodeZeroWebModule.cs" project="CodeZero.Web" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using CodeZero.Auditing;
using CodeZero.Modules;
using CodeZero.Runtime.Session;
using CodeZero.Web.Session;
using CodeZero.Configuration.Startup;
using CodeZero.Web.Configuration;
using CodeZero.Web.Security.AntiForgery;
using CodeZero.Collections.Extensions;
using CodeZero.Dependency;
using CodeZero.Web.MultiTenancy;

namespace CodeZero.Web
{
    /// <summary>
    /// This module is used to use CodeZero in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(CodeZeroWebCommonModule))]    
    public class CodeZeroWebModule : CodeZeroModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.Register<ICodeZeroAntiForgeryWebConfiguration, CodeZeroAntiForgeryWebConfiguration>();
            IocManager.Register<ICodeZeroWebLocalizationConfiguration, CodeZeroWebLocalizationConfiguration>();
            IocManager.Register<ICodeZeroWebModuleConfiguration, CodeZeroWebModuleConfiguration>();
            
            Configuration.ReplaceService<IPrincipalAccessor, HttpContextPrincipalAccessor>(DependencyLifeStyle.Transient);
            Configuration.ReplaceService<IClientInfoProvider, WebClientInfoProvider>(DependencyLifeStyle.Transient);

            Configuration.MultiTenancy.Resolvers.Add<DomainTenantResolveContributor>();
            Configuration.MultiTenancy.Resolvers.Add<HttpHeaderTenantResolveContributor>();
            Configuration.MultiTenancy.Resolvers.Add<HttpCookieTenantResolveContributor>();

            AddIgnoredTypes();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());            
        }

        private void AddIgnoredTypes()
        {
            var ignoredTypes = new[]
            {
                typeof(HttpPostedFileBase),
                typeof(IEnumerable<HttpPostedFileBase>),
                typeof(HttpPostedFileWrapper),
                typeof(IEnumerable<HttpPostedFileWrapper>)
            };
            
            foreach (var ignoredType in ignoredTypes)
            {
                Configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
                Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
            }
        }
    }
}
