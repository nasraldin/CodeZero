//  <copyright file="CodeZeroWebApiODataModule.cs" project="CodeZero.Web.Api.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Collections.Extensions;
using CodeZero.Dependency;
using CodeZero.Modules;
using CodeZero.WebApi.OData.Configuration;
using Microsoft.AspNet.OData;
using System.Reflection;

namespace CodeZero.WebApi.OData
{
    [DependsOn(typeof(CodeZeroWebApiModule))]
    public class CodeZeroWebApiODataModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ICodeZeroWebApiODataModuleConfiguration, CodeZeroWebApiODataModuleConfiguration>();

            Configuration.Validation.IgnoredTypes.AddIfNotContains(typeof(Delta));
        }

        public override void Initialize()
        {
            IocManager.Register<MetadataController>(DependencyLifeStyle.Transient);
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.CodeZeroWebApiOData().MapAction?.Invoke(Configuration);
        }
    }
}
