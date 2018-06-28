//  <copyright file="CodeZeroAspNetCoreODataModule.cs" project="CodeZero.AspNetCore.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using CodeZero.Collections.Extensions;
using CodeZero.Dependency;
using CodeZero.Modules;
using CodeZero.AspNetCore.OData.Configuration;
using Microsoft.AspNet.OData;

namespace CodeZero.AspNetCore.OData
{
    [DependsOn(typeof(CodeZeroAspNetCoreModule))]
    public class CodeZeroAspNetCoreODataModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ICodeZeroAspNetCoreODataModuleConfiguration, CodeZeroAspNetCoreODataModuleConfiguration>();

            Configuration.Validation.IgnoredTypes.AddIfNotContains(typeof(Delta));
        }

        public override void Initialize()
        {
            IocManager.Register<MetadataController>(DependencyLifeStyle.Transient);
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
