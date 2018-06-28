//  <copyright file="CodeZeroLdapModule.cs" project="CodeZero.Identity.Ldap" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Identity;
using CodeZero.Localization.Dictionaries.Xml;
using CodeZero.Localization.Sources;
using CodeZero.Modules;
using CodeZero.Identity.Ldap.Configuration;
using System.Reflection;

namespace CodeZero.Identity.Ldap
{
    /// <summary>
    /// This module extends module zero to add LDAP authentication.
    /// </summary>
    [DependsOn(typeof(CodeZeroCommonModule))]
    public class CodeZeroLdapModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ICodeZeroLdapModuleConfig, CodeZeroLdapModuleConfig>();

            Configuration.Localization.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo(
                    CodeZeroConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "CodeZero.Identity.Ldap.Localization.Source")
                    )
                );

            Configuration.Settings.Providers.Add<LdapSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
