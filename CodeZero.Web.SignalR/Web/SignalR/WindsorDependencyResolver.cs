//  <copyright file="WindsorDependencyResolver.cs" project="CodeZero.Web.SignalR" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using Microsoft.AspNet.SignalR;

namespace CodeZero.Web.SignalR
{
    /// <summary>
    /// Replaces <see cref="DefaultDependencyResolver"/> to resolve dependencies from Castle Windsor (<see cref="IWindsorContainer"/>).
    /// </summary>
    public class WindsorDependencyResolver : DefaultDependencyResolver
    {
        private readonly IWindsorContainer _windsorContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorDependencyResolver"/> class.
        /// </summary>
        /// <param name="windsorContainer">The windsor container.</param>
        public WindsorDependencyResolver(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer;
        }
        
        public override object GetService(Type serviceType)
        {
            return _windsorContainer.Kernel.HasComponent(serviceType)
                ? _windsorContainer.Resolve(serviceType)
                : base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _windsorContainer.Kernel.HasComponent(serviceType)
                ? _windsorContainer.ResolveAll(serviceType).Cast<object>()
                : base.GetServices(serviceType);
        }
    }
}