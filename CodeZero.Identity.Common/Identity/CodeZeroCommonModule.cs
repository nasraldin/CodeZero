//  <copyright file="CodeZeroCommonModule.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using System.Reflection;
using CodeZero.Application.Features;
using CodeZero.Auditing;
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.Collections.Extensions;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Localization;
using CodeZero.Localization.Dictionaries;
using CodeZero.Localization.Dictionaries.Xml;
using CodeZero.Modules;
using CodeZero.MultiTenancy;
using CodeZero.Reflection;
using CodeZero.Reflection.Extensions;
using CodeZero.Identity.Configuration;
using Castle.MicroKernel.Registration;

namespace CodeZero.Identity
{
    /// <summary>
    /// CodeZero Identity core module.
    /// </summary>
    [DependsOn(typeof(CodeZeroKernelModule))]
    public class CodeZeroCommonModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.RegisterIfNot<ICodeZeroEntityTypes, CodeZeroEntityTypes>(); //Registered on services.AddCodeZeroIdentity() for CodeZero.IdentityCore.

            IocManager.Register<IRoleManagementConfig, RoleManagementConfig>();
            IocManager.Register<IUserManagementConfig, UserManagementConfig>();
            IocManager.Register<ILanguageManagementConfig, LanguageManagementConfig>();
            IocManager.Register<ICodeZeroConfig, CodeZeroConfig>();

            Configuration.ReplaceService<ITenantStore, TenantStore>(DependencyLifeStyle.Transient);

            Configuration.Settings.Providers.Add<CodeZeroSettingProvider>();

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    CodeZeroConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CodeZeroCommonModule).GetAssembly(), "CodeZero.Identity.Localization.Source"
                        )));

            IocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            AddIgnoredTypes();
        }

        public override void Initialize()
        {
            FillMissingEntityTypes();

            IocManager.Register<IMultiTenantLocalizationDictionary, MultiTenantLocalizationDictionary>(DependencyLifeStyle.Transient);
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroCommonModule).GetAssembly());

            RegisterTenantCache();
        }

        private void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            if (typeof(ICodeZeroFeatureValueStore).IsAssignableFrom(handler.ComponentModel.Implementation) && !IocManager.IsRegistered<ICodeZeroFeatureValueStore>())
            {
                IocManager.IocContainer.Register(
                    Component.For<ICodeZeroFeatureValueStore>().ImplementedBy(handler.ComponentModel.Implementation).Named("CodeZeroFeatureValueStore").LifestyleTransient()
                    );
            }
        }

        private void AddIgnoredTypes()
        {
            var ignoredTypes = new[]
            {
                typeof(AuditLog)
            };

            foreach (var ignoredType in ignoredTypes)
            {
                Configuration.EntityHistory.IgnoredTypes.AddIfNotContains(ignoredType);
            }
        }

        private void FillMissingEntityTypes()
        {
            using (var entityTypes = IocManager.ResolveAsDisposable<ICodeZeroEntityTypes>())
            {
                if (entityTypes.Object.User != null &&
                    entityTypes.Object.Role != null &&
                    entityTypes.Object.Tenant != null)
                {
                    return;
                }

                using (var typeFinder = IocManager.ResolveAsDisposable<ITypeFinder>())
                {
                    var types = typeFinder.Object.FindAll();
                    entityTypes.Object.Tenant = types.FirstOrDefault(t => typeof(CodeZeroTenantBase).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract);
                    entityTypes.Object.Role = types.FirstOrDefault(t => typeof(CodeZeroRoleBase).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract);
                    entityTypes.Object.User = types.FirstOrDefault(t => typeof(CodeZeroUserBase).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract);
                }
            }
        }

        private void RegisterTenantCache()
        {
            if (IocManager.IsRegistered<ITenantCache>())
            {
                return;
            }

            using (var entityTypes = IocManager.ResolveAsDisposable<ICodeZeroEntityTypes>())
            {
                var implType = typeof (TenantCache<,>)
                    .MakeGenericType(entityTypes.Object.Tenant, entityTypes.Object.User);

                IocManager.Register(typeof (ITenantCache), implType, DependencyLifeStyle.Transient);
            }
        }
    }
}
