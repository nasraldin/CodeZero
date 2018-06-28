//  <copyright file="EntityHistoryInterceptorRegistrar.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using System.Reflection;
using CodeZero.Dependency;
using Castle.Core;

namespace CodeZero.EntityHistory
{
    internal static class EntityHistoryInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += (key, handler) =>
            {
                if (!iocManager.IsRegistered<IEntityHistoryConfiguration>())
                {
                    return;
                }

                var entityHistoryConfiguration = iocManager.Resolve<IEntityHistoryConfiguration>();

                if (ShouldIntercept(entityHistoryConfiguration, handler.ComponentModel.Implementation))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(EntityHistoryInterceptor)));
                }
            };
        }
        
        private static bool ShouldIntercept(IEntityHistoryConfiguration entityHistoryConfiguration, Type type)
        {
            if (type.GetTypeInfo().IsDefined(typeof(UseCaseAttribute), true))
            {
                return true;
            }

            if (type.GetMethods().Any(m => m.IsDefined(typeof(UseCaseAttribute), true)))
            {
                return true;
            }

            return false;
        }
    }
}
