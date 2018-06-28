//  <copyright file="CodeZeroWebCommonModule.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;
using CodeZero.Localization.Dictionaries;
using CodeZero.Localization.Dictionaries.Xml;
using CodeZero.Modules;
using CodeZero.Web.Api.ProxyScripting.Configuration;
using CodeZero.Web.Api.ProxyScripting.Generators.JQuery;
using CodeZero.Web.Configuration;
using CodeZero.Web.MultiTenancy;
using CodeZero.Web.Security.AntiForgery;
using CodeZero.Reflection.Extensions;

namespace CodeZero.Web
{
    /// <summary>
    /// This module is used to use CodeZero in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(CodeZeroKernelModule))]    
    public class CodeZeroWebCommonModule : CodeZeroModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.Register<IWebMultiTenancyConfiguration, WebMultiTenancyConfiguration>();
            IocManager.Register<IApiProxyScriptingConfiguration, ApiProxyScriptingConfiguration>();
            IocManager.Register<ICodeZeroAntiForgeryConfiguration, CodeZeroAntiForgeryConfiguration>();
            IocManager.Register<IWebEmbeddedResourcesConfiguration, WebEmbeddedResourcesConfiguration>();
            IocManager.Register<ICodeZeroWebCommonModuleConfiguration, CodeZeroWebCommonModuleConfiguration>();

            Configuration.Modules.CodeZeroWebCommon().ApiProxyScripting.Generators[JQueryProxyScriptGenerator.Name] = typeof(JQueryProxyScriptGenerator);

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    CodeZeroConsts.LocalizationCodeZeroWeb,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CodeZeroWebCommonModule).GetAssembly(), "CodeZero.Web.Localization.CodeZeroWebXmlSource"
                        )));
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroWebCommonModule).GetAssembly());            
        }
    }
}
