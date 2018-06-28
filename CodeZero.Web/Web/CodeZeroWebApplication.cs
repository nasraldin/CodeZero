//  <copyright file="CodeZeroWebApplication.cs" project="CodeZero.Web" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Web;
using CodeZero.Dependency;
using CodeZero.Modules;
using CodeZero.Threading;
using CodeZero.Web.Localization;

namespace CodeZero.Web
{
    /// <summary>
    /// This class is used to simplify starting of CodeZero system using <see cref="CodeZeroBootstrapper"/> class..
    /// Inherit from this class in global.asax instead of <see cref="HttpApplication"/> to be able to start CodeZero system.
    /// </summary>
    /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CodeZeroModule"/>.</typeparam>
    public abstract class CodeZeroWebApplication<TStartupModule> : HttpApplication
        where TStartupModule : CodeZeroModule
    {
        /// <summary>
        /// Gets a reference to the <see cref="CodeZeroBootstrapper"/> instance.
        /// </summary>
        public static CodeZeroBootstrapper CodeZeroBootstrapper { get; } = CodeZeroBootstrapper.Create<TStartupModule>();

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            ThreadCultureSanitizer.Sanitize();
            CodeZeroBootstrapper.Initialize();
        }

        protected virtual void Application_End(object sender, EventArgs e)
        {
            CodeZeroBootstrapper.Dispose();
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {

        }

        protected virtual void Session_End(object sender, EventArgs e)
        {

        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            SetCurrentCulture();
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {

        }

        protected virtual void SetCurrentCulture()
        {
            CodeZeroBootstrapper.IocManager.Using<ICurrentCultureSetter>(cultureSetter => cultureSetter.SetCurrentCulture(Context));
        }
    }
}
