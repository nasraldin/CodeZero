//  <copyright file="CodeZeroWebCommonModuleConfiguration.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.Web.Api.ProxyScripting.Configuration;
using CodeZero.Web.MultiTenancy;
using CodeZero.Web.Security.AntiForgery;

namespace CodeZero.Web.Configuration
{
    internal class CodeZeroWebCommonModuleConfiguration : ICodeZeroWebCommonModuleConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; }

        public IApiProxyScriptingConfiguration ApiProxyScripting { get; }

        public ICodeZeroAntiForgeryConfiguration AntiForgery { get; }

        public IWebEmbeddedResourcesConfiguration EmbeddedResources { get; }

        public IWebMultiTenancyConfiguration MultiTenancy { get; }

        public CodeZeroWebCommonModuleConfiguration(
            IApiProxyScriptingConfiguration apiProxyScripting, 
            ICodeZeroAntiForgeryConfiguration CodeZeroAntiForgery,
            IWebEmbeddedResourcesConfiguration embeddedResources, 
            IWebMultiTenancyConfiguration multiTenancy)
        {
            ApiProxyScripting = apiProxyScripting;
            AntiForgery = CodeZeroAntiForgery;
            EmbeddedResources = embeddedResources;
            MultiTenancy = multiTenancy;
        }
    }
}