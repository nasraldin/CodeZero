//  <copyright file="CodeZeroWebApiModule.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using CodeZero.Logging;
using CodeZero.Modules;
using CodeZero.Web;
using CodeZero.WebApi.Configuration;
using CodeZero.WebApi.Controllers;
using CodeZero.WebApi.Controllers.Dynamic;
using CodeZero.WebApi.Controllers.Dynamic.Formatters;
using CodeZero.WebApi.Controllers.Dynamic.Selectors;
using CodeZero.WebApi.Runtime.Caching;
using Castle.MicroKernel.Registration;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using CodeZero.Configuration.Startup;
using CodeZero.Json;
using CodeZero.WebApi.Auditing;
using CodeZero.WebApi.Authorization;
using CodeZero.WebApi.Controllers.ApiExplorer;
using CodeZero.WebApi.Controllers.Dynamic.Binders;
using CodeZero.WebApi.Controllers.Dynamic.Builders;
using CodeZero.WebApi.ExceptionHandling;
using CodeZero.WebApi.Security.AntiForgery;
using CodeZero.WebApi.Uow;
using CodeZero.WebApi.Validation;

namespace CodeZero.WebApi
{
    /// <summary>
    /// This module provides CodeZero features for ASP.NET Web API.
    /// </summary>
    [DependsOn(typeof(CodeZeroWebModule))]
    public class CodeZeroWebApiModule : CodeZeroModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());

            IocManager.Register<IDynamicApiControllerBuilder, DynamicApiControllerBuilder>();
            IocManager.Register<ICodeZeroWebApiConfiguration, CodeZeroWebApiConfiguration>();

            Configuration.Settings.Providers.Add<ClearCacheSettingProvider>();

            Configuration.Modules.CodeZeroWebApi().ResultWrappingIgnoreUrls.Add("/swagger");
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            var httpConfiguration = IocManager.Resolve<ICodeZeroWebApiConfiguration>().HttpConfiguration;

            InitializeAspNetServices(httpConfiguration);
            InitializeFilters(httpConfiguration);
            InitializeFormatters(httpConfiguration);
            InitializeRoutes(httpConfiguration);
            InitializeModelBinders(httpConfiguration);

            foreach (var controllerInfo in IocManager.Resolve<DynamicApiControllerManager>().GetAll())
            {
                IocManager.IocContainer.Register(
                    Component.For(controllerInfo.InterceptorType).LifestyleTransient(),
                    Component.For(controllerInfo.ApiControllerType)
                        .Proxy.AdditionalInterfaces(controllerInfo.ServiceInterfaceType)
                        .Interceptors(controllerInfo.InterceptorType)
                        .LifestyleTransient()
                    );

                LogHelper.Logger.DebugFormat("Dynamic web api controller is created for type '{0}' with service name '{1}'.", controllerInfo.ServiceInterfaceType.FullName, controllerInfo.ServiceName);
            }

            Configuration.Modules.CodeZeroWebApi().HttpConfiguration.EnsureInitialized();
        }

        private void InitializeAspNetServices(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new CodeZeroHttpControllerSelector(httpConfiguration, IocManager.Resolve<DynamicApiControllerManager>()));
            httpConfiguration.Services.Replace(typeof(IHttpActionSelector), new CodeZeroApiControllerActionSelector(IocManager.Resolve<ICodeZeroWebApiConfiguration>()));
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), new CodeZeroApiControllerActivator(IocManager));
            httpConfiguration.Services.Replace(typeof(IApiExplorer), IocManager.Resolve<CodeZeroApiExplorer>());
        }

        private void InitializeFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Filters.Add(IocManager.Resolve<CodeZeroApiAuthorizeFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<CodeZeroAntiForgeryApiFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<CodeZeroApiAuditFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<CodeZeroApiValidationFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<CodeZeroApiUowFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<CodeZeroApiExceptionFilterAttribute>());

            httpConfiguration.MessageHandlers.Add(IocManager.Resolve<ResultWrapperHandler>());
        }

        private static void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            //Remove formatters except JsonFormatter.
            foreach (var currentFormatter in httpConfiguration.Formatters.ToList())
            {
                if (!(currentFormatter is JsonMediaTypeFormatter || 
                    currentFormatter is JQueryMvcFormUrlEncodedFormatter))
                {
                    httpConfiguration.Formatters.Remove(currentFormatter);
                }
            }

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CodeZeroCamelCasePropertyNamesContractResolver();
            httpConfiguration.Formatters.Add(new PlainTextFormatter());
        }

        private static void InitializeRoutes(HttpConfiguration httpConfiguration)
        {
            //Dynamic Web APIs

            httpConfiguration.Routes.MapHttpRoute(
                name: "CodeZeroDynamicWebApi",
                routeTemplate: "api/services/{*serviceNameWithAction}"
                );

            //Other routes

            httpConfiguration.Routes.MapHttpRoute(
                name: "CodeZeroCacheController_Clear",
                routeTemplate: "api/CodeZeroCache/Clear",
                defaults: new { controller = "CodeZeroCache", action = "Clear" }
                );

            httpConfiguration.Routes.MapHttpRoute(
                name: "CodeZeroCacheController_ClearAll",
                routeTemplate: "api/CodeZeroCache/ClearAll",
                defaults: new { controller = "CodeZeroCache", action = "ClearAll" }
                );
        }

        private static void InitializeModelBinders(HttpConfiguration httpConfiguration)
        {
            var CodeZeroApiDateTimeBinder = new CodeZeroApiDateTimeBinder();
            httpConfiguration.BindParameter(typeof(DateTime), CodeZeroApiDateTimeBinder);
            httpConfiguration.BindParameter(typeof(DateTime?), CodeZeroApiDateTimeBinder);
        }
    }
}
