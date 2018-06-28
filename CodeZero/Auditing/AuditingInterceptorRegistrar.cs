//  <copyright file="AuditingInterceptorRegistrar.cs" project="CodeZero" solution="CodeZero">
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

namespace CodeZero.Auditing
{
    internal static class AuditingInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += (key, handler) =>
            {
                if (!iocManager.IsRegistered<IAuditingConfiguration>())
                {
                    return;
                }

                var auditingConfiguration = iocManager.Resolve<IAuditingConfiguration>();

                if (ShouldIntercept(auditingConfiguration, handler.ComponentModel.Implementation))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(AuditingInterceptor)));
                }
            };
        }
        
        private static bool ShouldIntercept(IAuditingConfiguration auditingConfiguration, Type type)
        {
            if (auditingConfiguration.Selectors.Any(selector => selector.Predicate(type)))
            {
                return true;
            }

            if (type.GetTypeInfo().IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (type.GetMethods().Any(m => m.IsDefined(typeof(AuditedAttribute), true)))
            {
                return true;
            }

            return false;
        }
    }
}