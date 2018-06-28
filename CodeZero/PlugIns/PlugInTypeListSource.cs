//  <copyright file="PlugInTypeListSource.cs" project="CodeZero" solution="CodeZero">
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
using System.Reflection;

namespace CodeZero.PlugIns
{
    public class PlugInTypeListSource : IPlugInSource
    {
        private readonly Type[] _moduleTypes;
        private readonly Lazy<List<Assembly>> _assemblies;

        public PlugInTypeListSource(params Type[] moduleTypes)
        {
            _moduleTypes = moduleTypes;

            _assemblies = new Lazy<List<Assembly>>(LoadAssemblies, true);
        }

        public List<Assembly> GetAssemblies()
        {
            return _assemblies.Value;
        }

        public List<Type> GetModules()
        {
            return _moduleTypes.ToList();
        }

        private List<Assembly> LoadAssemblies()
        {
            return _moduleTypes.Select(type => type.GetTypeInfo().Assembly).ToList();
        }
    }
}