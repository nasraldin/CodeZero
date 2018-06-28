//  <copyright file="EventBusInstaller.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.Events.Bus.Factories;
using CodeZero.Events.Bus.Handlers;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace CodeZero.Events.Bus
{
    /// <summary>
    /// Installs event bus system and registers all handlers automatically.
    /// </summary>
    internal class EventBusInstaller : IWindsorInstaller
    {
        private readonly IIocResolver _iocResolver;
        private readonly IEventBusConfiguration _eventBusConfiguration;
        private IEventBus _eventBus;

        public EventBusInstaller(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
            _eventBusConfiguration = iocResolver.Resolve<IEventBusConfiguration>();
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (_eventBusConfiguration.UseDefaultEventBus)
            {
                container.Register(
                    Component.For<IEventBus>().UsingFactoryMethod(() => EventBus.Default).LifestyleSingleton()
                    );
            }
            else
            {
                container.Register(
                    Component.For<IEventBus>().ImplementedBy<EventBus>().LifestyleSingleton()
                    );
            }

            _eventBus = container.Resolve<IEventBus>();

            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            /* This code checks if registering component implements any IEventHandler<TEventData> interface, if yes,
             * gets all event handler interfaces and registers type to Event Bus for each handling event.
             */
            if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                return;
            }

            var interfaces = handler.ComponentModel.Implementation.GetTypeInfo().GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                {
                    continue;
                }

                var genericArgs = @interface.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    _eventBus.Register(genericArgs[0], new IocHandlerFactory(_iocResolver, handler.ComponentModel.Implementation));
                }
            }
        }
    }
}
