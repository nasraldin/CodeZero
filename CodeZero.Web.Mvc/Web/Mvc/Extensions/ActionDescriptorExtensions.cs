//  <copyright file="ActionDescriptorExtensions.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using CodeZero.Extensions;

namespace CodeZero.Web.Mvc.Extensions
{
    public static class ActionDescriptorExtensions
    {
        public static MethodInfo GetMethodInfoOrNull(this ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ReflectedActionDescriptor)
            {
                return actionDescriptor.As<ReflectedActionDescriptor>().MethodInfo;
            }

            if (actionDescriptor is ReflectedAsyncActionDescriptor)
            {
                return actionDescriptor.As<ReflectedAsyncActionDescriptor>().MethodInfo;
            }

            if (actionDescriptor is TaskAsyncActionDescriptor)
            {
                return actionDescriptor.As<TaskAsyncActionDescriptor>().MethodInfo;
            }

            return null;
        }
    }
}