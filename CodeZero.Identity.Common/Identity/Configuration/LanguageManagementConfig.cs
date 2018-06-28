//  <copyright file="LanguageManagementConfig.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Localization;
using CodeZero.Localization.Dictionaries;
using Castle.Core.Logging;

namespace CodeZero.Identity.Configuration
{
    internal class LanguageManagementConfig : ILanguageManagementConfig
    {
        public ILogger Logger { get; set; }

        private readonly IIocManager _iocManager;
        private readonly ICodeZeroStartupConfiguration _configuration;

        public LanguageManagementConfig(IIocManager iocManager, ICodeZeroStartupConfiguration configuration)
        {
            _iocManager = iocManager;
            _configuration = configuration;

            Logger = NullLogger.Instance;
        }

        public void EnableDbLocalization()
        {
            _iocManager.Register<ILanguageProvider, ApplicationLanguageProvider>(DependencyLifeStyle.Transient);

            var sources = _configuration
                .Localization
                .Sources
                .Where(s => s is IDictionaryBasedLocalizationSource)
                .Cast<IDictionaryBasedLocalizationSource>()
                .ToList();
            
            foreach (var source in sources)
            {
                _configuration.Localization.Sources.Remove(source);
                _configuration.Localization.Sources.Add(
                    new MultiTenantLocalizationSource(
                        source.Name,
                        new MultiTenantLocalizationDictionaryProvider(
                            source.DictionaryProvider,
                            _iocManager
                            )
                        )
                    );

                Logger.DebugFormat("Converted {0} ({1}) to MultiTenantLocalizationSource", source.Name, source.GetType());
            }
        }
    }
}