//  <copyright file="ICodeZeroWebCommonModuleConfiguration.cs" project="CodeZero.Web.Common" solution="CodeZero">
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
    /// <summary>
    /// Used to configure CodeZero Web Common module.
    /// </summary>
    public interface ICodeZeroWebCommonModuleConfiguration
    {
        /// <summary>
        /// If this is set to true, all exception and details are sent directly to clients on an error.
        /// Default: false (CodeZero hides exception details from clients except special exceptions.)
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }

        /// <summary>
        /// Used to configure Api proxy scripting.
        /// </summary>
        IApiProxyScriptingConfiguration ApiProxyScripting { get; }

        /// <summary>
        /// Used to configure Anti Forgery security settings.
        /// </summary>
        ICodeZeroAntiForgeryConfiguration AntiForgery { get; }

        /// <summary>
        /// Used to configure embedded resource system for web applications.
        /// </summary>
        IWebEmbeddedResourcesConfiguration EmbeddedResources { get; }

        IWebMultiTenancyConfiguration MultiTenancy { get; }
    }
}