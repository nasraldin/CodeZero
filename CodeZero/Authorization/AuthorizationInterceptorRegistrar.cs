//  <copyright file="AuthorizationInterceptorRegistrar.cs" project="CodeZero" solution="CodeZero">
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
using CodeZero.Application.Features;
using CodeZero.Dependency;
using Castle.Core;
using Castle.MicroKernel;

namespace CodeZero.Authorization
{
    /// <summary>
    /// This class is used to register interceptors on the Application Layer.
    /// </summary>
    internal static class AuthorizationInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;            
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (ShouldIntercept(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(AuthorizationInterceptor))); 
            }
        }

        private static bool ShouldIntercept(Type type)
        {
            if (SelfOrMethodsDefinesAttribute<CodeZeroAuthorizeAttribute>(type))
            {
                return true;
            }

            if (SelfOrMethodsDefinesAttribute<RequiresFeatureAttribute>(type))
            {
                return true;
            }

            return false;
        }

        private static bool SelfOrMethodsDefinesAttribute<TAttr>(Type type)
        {
            if (type.GetTypeInfo().IsDefined(typeof(TAttr), true))
            {
                return true;
            }

            return type
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Any(m => m.IsDefined(typeof(TAttr), true));
        }
    }
}