//  <copyright file="CodeZeroPlugInManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;

namespace CodeZero.PlugIns
{
    public class CodeZeroPlugInManager : ICodeZeroPlugInManager
    {
        public PlugInSourceList PlugInSources { get; }

        private static readonly object SyncObj = new object();
        private static bool _isRegisteredToAssemblyResolve;

        public CodeZeroPlugInManager()
        {
            PlugInSources = new PlugInSourceList();

            //TODO: Try to use AssemblyLoadContext.Default..?
            RegisterToAssemblyResolve(PlugInSources);
        }

        private static void RegisterToAssemblyResolve(PlugInSourceList plugInSources)
        {
            if (_isRegisteredToAssemblyResolve)
            {
                return;
            }

            lock (SyncObj)
            {
                if (_isRegisteredToAssemblyResolve)
                {
                    return;
                }

                _isRegisteredToAssemblyResolve = true;

                AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
                {
                    return plugInSources.GetAllAssemblies().FirstOrDefault(a => a.FullName == args.Name);
                };
            }
        }
    }
}