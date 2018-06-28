//  <copyright file="CodeZeroAspNetCoreSignalRModule.cs" project="CodeZero.AspNetCore.SignalR" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using CodeZero.Modules;

namespace CodeZero.AspNetCore.SignalR
{
    /// <summary>
    /// CodeZero ASP.NET Core SignalR integration module.
    /// </summary>
    [DependsOn(typeof(CodeZeroKernelModule))]
    public class CodeZeroAspNetCoreSignalRModule : CodeZeroModule
    {
        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
