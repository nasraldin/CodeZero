//  <copyright file="CodeZeroMemoryDbModule.cs" project="CodeZero.MemoryDb" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using CodeZero.MemoryDb.Configuration;
using CodeZero.Modules;
using CodeZero.Reflection.Extensions;

namespace CodeZero.MemoryDb
{
    /// <summary>
    /// This module is used to implement "Data Access Layer" in MemoryDb.
    /// </summary>
    public class CodeZeroMemoryDbModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ICodeZeroMemoryDbModuleConfiguration, CodeZeroMemoryDbModuleConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroMemoryDbModule).GetAssembly());
        }
    }
}
