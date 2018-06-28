//  <copyright file="UnitOfWorkDefaultOptionsExtensions.cs" project="CodeZero" solution="CodeZero">
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

namespace CodeZero.Domain.Uow
{
    public static class UnitOfWorkDefaultOptionsExtensions
    {
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(this IUnitOfWorkDefaultOptions unitOfWorkDefaultOptions, MethodInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(true).OfType<UnitOfWorkAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            }

            attrs = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<UnitOfWorkAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            }

            if (unitOfWorkDefaultOptions.IsConventionalUowClass(methodInfo.DeclaringType))
            {
                return new UnitOfWorkAttribute(); //Default
            }

            return null;
        }

        public static bool IsConventionalUowClass(this IUnitOfWorkDefaultOptions unitOfWorkDefaultOptions, Type type)
        {
            return unitOfWorkDefaultOptions.ConventionalUowSelectors.Any(selector => selector(type));
        }
    }
}
