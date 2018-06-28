//  <copyright file="CodeZeroApplicationBuilderExtensions.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using CodeZero.AspNetCore.EmbeddedResources;
using CodeZero.AspNetCore.Localization;
using CodeZero.Dependency;
using CodeZero.Localization;
using Castle.LoggingFacility.MsLogging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using CodeZero.AspNetCore.Security;
using Microsoft.AspNetCore.Hosting;

namespace CodeZero.AspNetCore
{
    public static class CodeZeroApplicationBuilderExtensions
    {
        public static void UseCodeZero(this IApplicationBuilder app)
        {
            app.UseCodeZero(null);
        }

        public static void UseCodeZero([NotNull] this IApplicationBuilder app, Action<CodeZeroApplicationBuilderOptions> optionsAction)
        {
            Check.NotNull(app, nameof(app));

            var options = new CodeZeroApplicationBuilderOptions();
            optionsAction?.Invoke(options);

            if (options.UseCastleLoggerFactory)
            {
                app.UseCastleLoggerFactory();
            }

            InitializeCodeZero(app);

            if (options.UseCodeZeroRequestLocalization)
            {
                //TODO: This should be added later than authorization middleware!
                app.UseCodeZeroRequestLocalization();
            }

            if (options.UseSecurityHeaders)
            {
                app.UseCodeZeroSecurityHeaders();
            }
        }

        public static void UseEmbeddedFiles(this IApplicationBuilder app)
        {
            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new EmbeddedResourceFileProvider(
                        app.ApplicationServices.GetRequiredService<IIocResolver>()
                    )
                }
            );
        }

        private static void InitializeCodeZero(IApplicationBuilder app)
        {
            var CodeZeroBootstrapper = app.ApplicationServices.GetRequiredService<CodeZeroBootstrapper>();
            CodeZeroBootstrapper.Initialize();

            var applicationLifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(() => CodeZeroBootstrapper.Dispose());
        }

        public static void UseCastleLoggerFactory(this IApplicationBuilder app)
        {
            var castleLoggerFactory = app.ApplicationServices.GetService<Castle.Core.Logging.ILoggerFactory>();
            if (castleLoggerFactory == null)
            {
                return;
            }

            app.ApplicationServices
                .GetRequiredService<ILoggerFactory>()
                .AddCastleLogger(castleLoggerFactory);
        }

        public static void UseCodeZeroRequestLocalization(this IApplicationBuilder app, Action<RequestLocalizationOptions> optionsAction = null)
        {
            var iocResolver = app.ApplicationServices.GetRequiredService<IIocResolver>();
            using (var languageManager = iocResolver.ResolveAsDisposable<ILanguageManager>())
            {
                var supportedCultures = languageManager.Object
                    .GetLanguages()
                    .Select(l => CultureInfo.GetCultureInfo(l.Name))
                    .ToArray();

                var options = new RequestLocalizationOptions
                {
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                };

                var userProvider = new CodeZeroUserRequestCultureProvider();

                //0: QueryStringRequestCultureProvider
                options.RequestCultureProviders.Insert(1, userProvider);
                options.RequestCultureProviders.Insert(2, new CodeZeroLocalizationHeaderRequestCultureProvider());
                //3: CookieRequestCultureProvider
                options.RequestCultureProviders.Insert(4, new CodeZeroDefaultRequestCultureProvider());
                //5: AcceptLanguageHeaderRequestCultureProvider

                optionsAction?.Invoke(options);

                userProvider.CookieProvider = options.RequestCultureProviders.OfType<CookieRequestCultureProvider>().FirstOrDefault();
                userProvider.HeaderProvider = options.RequestCultureProviders.OfType<CodeZeroLocalizationHeaderRequestCultureProvider>().FirstOrDefault();

                app.UseRequestLocalization(options);
            }
        }

        public static void UseCodeZeroSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseMiddleware<CodeZeroSecurityHeadersMiddleware>();
        }
    }
}
