//  <copyright file="CodeZeroOwinExtensions.cs" project="CodeZero.Owin" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;
using CodeZero.Modules;
using CodeZero.Owin.EmbeddedResources;
using CodeZero.Resources.Embedded;
using CodeZero.Threading;
using CodeZero.Web.Configuration;
using JetBrains.Annotations;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Web;

namespace CodeZero.Owin
{
    /// <summary>
    /// OWIN extension methods for CodeZero.
    /// </summary>
    public static class CodeZeroOwinExtensions
    {
        /// <summary>
        /// This should be called as the first line for OWIN based applications for CodeZero framework.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void UseCodeZero(this IAppBuilder app)
        {
            app.UseCodeZero(null);
        }

        public static void UseCodeZero(this IAppBuilder app, [CanBeNull] Action<CodeZeroOwinOptions> optionsAction)
        {
            ThreadCultureSanitizer.Sanitize();

            var options = new CodeZeroOwinOptions
            {
                UseEmbeddedFiles = HttpContext.Current?.Server != null
            };

            optionsAction?.Invoke(options);

            if (options.UseEmbeddedFiles)
            {
                if (HttpContext.Current?.Server == null)
                {
                    throw new CodeZeroInitializationException("Can not enable UseEmbeddedFiles for OWIN since HttpContext.Current is null! If you are using ASP.NET Core, serve embedded resources through ASP.NET Core middleware instead of OWIN. See http://www.aspnetboilerplate.com/Pages/Documents/Embedded-Resource-Files#aspnet-core-configuration");
                }

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileSystem = new CodeZeroOwinEmbeddedResourceFileSystem(
                        IocManager.Instance.Resolve<IEmbeddedResourceManager>(),
                        IocManager.Instance.Resolve<IWebEmbeddedResourcesConfiguration>(),
                        HttpContext.Current.Server.MapPath("~/")
                    )
                });
            }
        }

        /// <summary>
        /// Use this extension method if you don't initialize CodeZero in other way.
        /// Otherwise, use <see cref="UseCodeZero(IAppBuilder)"/>.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <typeparam name="TStartupModule">The type of the startup module.</typeparam>
        public static void UseCodeZero<TStartupModule>(this IAppBuilder app)
            where TStartupModule : CodeZeroModule
        {
            app.UseCodeZero<TStartupModule>(null, null);
        }

        /// <summary>
        /// Use this extension method if you don't initialize CodeZero in other way.
        /// Otherwise, use <see cref="UseCodeZero(IAppBuilder)"/>.
        /// </summary>
        /// <typeparam name="TStartupModule">The type of the startup module.</typeparam>
        public static void UseCodeZero<TStartupModule>(this IAppBuilder app, [CanBeNull] Action<CodeZeroBootstrapper> configureAction, [CanBeNull] Action<CodeZeroOwinOptions> optionsAction = null)
            where TStartupModule : CodeZeroModule
        {
            app.UseCodeZero(optionsAction);

            if (!app.Properties.ContainsKey("_CodeZeroBootstrapper.Instance"))
            {
                var codeZeroBootstrapper = CodeZeroBootstrapper.Create<TStartupModule>();
                app.Properties["_CodeZeroBootstrapper.Instance"] = codeZeroBootstrapper;
                configureAction?.Invoke(codeZeroBootstrapper);
                codeZeroBootstrapper.Initialize();
            }
        }
    }
}