//  <copyright file="CodeZeroBootstrapper.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Reflection;
using CodeZero.Auditing;
using CodeZero.Authorization;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Dependency.Installers;
using CodeZero.Domain.Uow;
using CodeZero.EntityHistory;
using CodeZero.Modules;
using CodeZero.PlugIns;
using CodeZero.Runtime.Validation.Interception;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using JetBrains.Annotations;

namespace CodeZero
{
    /// <summary>
    /// This is the main class that is responsible to start entire CodeZero system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// </summary>
    public class CodeZeroBootstrapper : IDisposable
    {
        /// <summary>
        /// Get the startup module of the application which depends on other used modules.
        /// </summary>
        public Type StartupModule { get; }

        /// <summary>
        /// A list of plug in folders.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool IsDisposed;

        private CodeZeroModuleManager _moduleManager;
        private ILogger _logger;

        /// <summary>
        /// Creates a new <see cref="CodeZeroBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CodeZeroModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        private CodeZeroBootstrapper([NotNull] Type startupModule, [CanBeNull] Action<CodeZeroBootstrapperOptions> optionsAction = null)
        {
            Check.NotNull(startupModule, nameof(startupModule));

            var options = new CodeZeroBootstrapperOptions();
            optionsAction?.Invoke(options);

            if (!typeof(CodeZeroModule).GetTypeInfo().IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(CodeZeroModule)}.");
            }

            StartupModule = startupModule;

            IocManager = options.IocManager;
            PlugInSources = options.PlugInSources;

            _logger = NullLogger.Instance;

            if (!options.DisableAllInterceptors)
            {
                AddInterceptorRegistrars();
            }
        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CodeZeroModule"/>.</typeparam>
        /// <param name="optionsAction">An action to set options</param>
        public static CodeZeroBootstrapper Create<TStartupModule>([CanBeNull] Action<CodeZeroBootstrapperOptions> optionsAction = null)
            where TStartupModule : CodeZeroModule
        {
            return new CodeZeroBootstrapper(typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CodeZeroModule"/>.</param>
        /// <param name="optionsAction">An action to set options</param>
        public static CodeZeroBootstrapper Create([NotNull] Type startupModule, [CanBeNull] Action<CodeZeroBootstrapperOptions> optionsAction = null)
        {
            return new CodeZeroBootstrapper(startupModule, optionsAction);
        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CodeZeroModule"/>.</typeparam>
        /// <param name="iocManager">IIocManager that is used to bootstrap the CodeZero system</param>
        [Obsolete("Use overload with parameter type: Action<CodeZeroBootstrapperOptions> optionsAction")]
        public static CodeZeroBootstrapper Create<TStartupModule>([NotNull] IIocManager iocManager)
            where TStartupModule : CodeZeroModule
        {
            return new CodeZeroBootstrapper(typeof(TStartupModule), options =>
            {
                options.IocManager = iocManager;
            });
        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="CodeZeroModule"/>.</param>
        /// <param name="iocManager">IIocManager that is used to bootstrap the CodeZero system</param>
        [Obsolete("Use overload with parameter type: Action<CodeZeroBootstrapperOptions> optionsAction")]
        public static CodeZeroBootstrapper Create([NotNull] Type startupModule, [NotNull] IIocManager iocManager)
        {
            return new CodeZeroBootstrapper(startupModule, options =>
            {
                options.IocManager = iocManager;
            });
        }

        private void AddInterceptorRegistrars()
        {
            ValidationInterceptorRegistrar.Initialize(IocManager);
            AuditingInterceptorRegistrar.Initialize(IocManager);
            EntityHistoryInterceptorRegistrar.Initialize(IocManager);
            UnitOfWorkRegistrar.Initialize(IocManager);
            AuthorizationInterceptorRegistrar.Initialize(IocManager);
        }

        /// <summary>
        /// Initializes the CodeZero system.
        /// </summary>
        public virtual void Initialize()
        {
            ResolveLogger();

            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new CodeZeroCoreInstaller());

                IocManager.Resolve<CodeZeroPlugInManager>().PlugInSources.AddRange(PlugInSources);
                IocManager.Resolve<CodeZeroStartupConfiguration>().Initialize();

                _moduleManager = IocManager.Resolve<CodeZeroModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(CodeZeroBootstrapper));
            }
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<CodeZeroBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<CodeZeroBootstrapper>().Instance(this)
                    );
            }
        }

        /// <summary>
        /// Disposes the CodeZero system.
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            _moduleManager?.ShutdownModules();
        }
    }
}
