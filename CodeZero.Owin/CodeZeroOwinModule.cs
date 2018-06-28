//  <copyright file="CodeZeroOwinModule.cs" project="CodeZero.Owin" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using CodeZero.Modules;
using CodeZero.Web;

namespace CodeZero.Owin
{
    /// <summary>
    /// OWIN integration module for CodeZero.
    /// </summary>
    [DependsOn(typeof (CodeZeroWebCommonModule))]
    public class CodeZeroOwinModule : CodeZeroModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
