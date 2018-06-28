//  <copyright file="CodeZeroWebSignalRModule.cs" project="CodeZero.Web.SignalR" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using CodeZero.Modules;
using Castle.MicroKernel.Registration;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace CodeZero.Web.SignalR
{
    /// <summary>
    /// CodeZero SignalR integration module.
    /// </summary>
    [DependsOn(typeof(CodeZeroKernelModule))]
    public class CodeZeroWebSignalRModule : CodeZeroModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            GlobalHost.DependencyResolver = new WindsorDependencyResolver(IocManager.IocContainer);
            UseCodeZeroSignalRContractResolver();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        private void UseCodeZeroSignalRContractResolver()
        {
            var serializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    ContractResolver = new CodeZeroSignalRContractResolver()
                });
            
            IocManager.IocContainer.Register(
                Component.For<JsonSerializer>().UsingFactoryMethod(() => serializer)
                );
        }
    }
}
