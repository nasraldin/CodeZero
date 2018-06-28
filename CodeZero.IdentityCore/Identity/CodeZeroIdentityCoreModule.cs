//  <copyright file="CodeZeroIdentityCoreModule.cs" project="CodeZero.IdentityCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Localization.Dictionaries.Xml;
using CodeZero.Localization.Sources;
using CodeZero.Modules;
using CodeZero.Reflection.Extensions;

namespace CodeZero.Identity
{
    [DependsOn(typeof(CodeZeroCommonModule))]
    public class CodeZeroIdentityCoreModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo(
                    CodeZeroConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CodeZeroIdentityCoreModule).GetAssembly(), "CodeZero.Identity.Localization.SourceExt"
                    )
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroIdentityCoreModule).GetAssembly());
        }
    }
}
