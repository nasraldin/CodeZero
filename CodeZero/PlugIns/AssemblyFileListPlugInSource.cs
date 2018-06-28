//  <copyright file="AssemblyFileListPlugInSource.cs" project="CodeZero" solution="CodeZero">
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
using CodeZero.Collections.Extensions;
using CodeZero.Modules;

namespace CodeZero.PlugIns
{
    //TODO: This class is similar to FolderPlugInSource. Create an abstract base class for them.
    public class AssemblyFileListPlugInSource : IPlugInSource
    {
        public string[] FilePaths { get; }

        private readonly Lazy<List<Assembly>> _assemblies;

        public AssemblyFileListPlugInSource(params string[] filePaths)
        {
            FilePaths = filePaths ?? new string[0];

            _assemblies = new Lazy<List<Assembly>>(LoadAssemblies, true);
        }

        public List<Assembly> GetAssemblies()
        {
            return _assemblies.Value;
        }

        public List<Type> GetModules()
        {
            var modules = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (CodeZeroModule.IsCodeZeroModule(type))
                        {
                            modules.AddIfNotContains(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new CodeZeroInitializationException("Could not get module types from assembly: " + assembly.FullName, ex);
                }
            }

            return modules;
        }

        private List<Assembly> LoadAssemblies()
        {
            return FilePaths.Select(
                Assembly.LoadFile //TODO: Use AssemblyLoadContext.Default.LoadFromAssemblyPath instead?
            ).ToList();
        }
    }
}