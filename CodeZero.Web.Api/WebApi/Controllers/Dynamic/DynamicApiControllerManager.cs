//  <copyright file="DynamicApiControllerManager.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using CodeZero.Collections.Extensions;
using CodeZero.Dependency;

namespace CodeZero.WebApi.Controllers.Dynamic
{
    /// <summary>
    /// This class is used to store dynamic controller information.
    /// </summary>
    public class DynamicApiControllerManager : ISingletonDependency
    {
        private readonly IDictionary<string, DynamicApiControllerInfo> _dynamicApiControllers;

        public DynamicApiControllerManager()
        {
            _dynamicApiControllers = new Dictionary<string, DynamicApiControllerInfo>(StringComparer.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Registers given controller info to be found later.
        /// </summary>
        /// <param name="controllerInfo">Controller info</param>
        public void Register(DynamicApiControllerInfo controllerInfo)
        {
            _dynamicApiControllers[controllerInfo.ServiceName] = controllerInfo;
        }

        /// <summary>
        /// Searches and returns a dynamic api controller for given name.
        /// </summary>
        /// <param name="controllerName">Name of the controller</param>
        /// <returns>Controller info</returns>
        public DynamicApiControllerInfo FindOrNull(string controllerName)
        {
            return _dynamicApiControllers.GetOrDefault(controllerName);
        }

        public IReadOnlyList<DynamicApiControllerInfo> GetAll()
        {
            return _dynamicApiControllers.Values.ToImmutableList();
        }
    }
}