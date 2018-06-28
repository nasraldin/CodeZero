//  <copyright file="CodeZeroServiceCollectionExtensions.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Castle.Windsor.MsDependencyInjection;
using CodeZero.AspNetCore.EmbeddedResources;
using CodeZero.AspNetCore.Mvc;
using CodeZero.AspNetCore.Mvc.Antiforgery;
using CodeZero.AspNetCore.Mvc.Providers;
using CodeZero.Dependency;
using CodeZero.Json;
using CodeZero.Modules;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using System;

namespace CodeZero.AspNetCore
{
    public static class CodeZeroServiceCollectionExtensions
    {
        /// <summary>
        /// Integrates CodeZero to AspNet Core.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CodeZeroModule"/>.</typeparam>
        /// <param name="services">Services.</param>
        /// <param name="optionsAction">An action to get/modify options</param>
        public static IServiceProvider AddCodeZero<TStartupModule>(this IServiceCollection services, [CanBeNull] Action<CodeZeroBootstrapperOptions> optionsAction = null)
            where TStartupModule : CodeZeroModule
        {
            var CodeZeroBootstrapper = AddCodeZeroBootstrapper<TStartupModule>(services, optionsAction);

            ConfigureAspNetCore(services, CodeZeroBootstrapper.IocManager);

            return WindsorRegistrationHelper.CreateServiceProvider(CodeZeroBootstrapper.IocManager.IocContainer, services);
        }

        private static void ConfigureAspNetCore(IServiceCollection services, IIocResolver iocResolver)
        {
            //See https://github.com/aspnet/Mvc/issues/3936 to know why we added these services.
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Use DI to create controllers
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //Use DI to create view components
            services.Replace(ServiceDescriptor.Singleton<IViewComponentActivator, ServiceBasedViewComponentActivator>());

            //Change anti forgery filters (to work proper with non-browser clients)
            services.Replace(ServiceDescriptor.Transient<AutoValidateAntiforgeryTokenAuthorizationFilter, CodeZeroAutoValidateAntiforgeryTokenAuthorizationFilter>());
            services.Replace(ServiceDescriptor.Transient<ValidateAntiforgeryTokenAuthorizationFilter, CodeZeroValidateAntiforgeryTokenAuthorizationFilter>());

            //Add feature providers
            var partManager = services.GetSingletonServiceOrNull<ApplicationPartManager>();
            partManager?.FeatureProviders.Add(new CodeZeroAppServiceControllerFeatureProvider(iocResolver));

            //Configure JSON serializer
            services.Configure<MvcJsonOptions>(jsonOptions =>
            {
                jsonOptions.SerializerSettings.ContractResolver = new CodeZeroContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            //Configure MVC
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddCodeZero(services);
            });

            //Configure Razor
            services.Insert(0,
                ServiceDescriptor.Singleton<IConfigureOptions<RazorViewEngineOptions>>(
                    new ConfigureOptions<RazorViewEngineOptions>(
                        (options) =>
                        {
                            options.FileProviders.Add(new EmbeddedResourceViewFileProvider(iocResolver));
                        }
                    )
                )
            );
        }

        private static CodeZeroBootstrapper AddCodeZeroBootstrapper<TStartupModule>(IServiceCollection services, Action<CodeZeroBootstrapperOptions> optionsAction)
            where TStartupModule : CodeZeroModule
        {
            var codeZeroBootstrapper = CodeZeroBootstrapper.Create<TStartupModule>(optionsAction);
            services.AddSingleton(codeZeroBootstrapper);
            return codeZeroBootstrapper;
        }
    }
}