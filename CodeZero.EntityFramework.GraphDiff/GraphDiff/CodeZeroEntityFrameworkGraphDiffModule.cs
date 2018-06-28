//  <copyright file="CodeZeroEntityFrameworkGraphDiffModule.cs" project="CodeZero.EntityFramework.GraphDiff" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Reflection;
using CodeZero.EntityFramework.GraphDiff.Configuration;
using CodeZero.EntityFramework.GraphDiff.Mapping;
using CodeZero.Modules;

namespace CodeZero.EntityFramework.GraphDiff
{
    [DependsOn(typeof(CodeZeroEntityFrameworkModule), typeof(CodeZeroKernelModule))]
    public class CodeZeroEntityFrameworkGraphDiffModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ICodeZeroEntityFrameworkGraphDiffModuleConfiguration, CodeZeroEntityFrameworkGraphDiffModuleConfiguration>();

            Configuration.Modules
                .CodeZeroEfGraphDiff()
                .UseMappings(new List<EntityMapping>());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
