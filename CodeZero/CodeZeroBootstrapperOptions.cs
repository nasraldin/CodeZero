//  <copyright file="CodeZeroBootstrapperOptions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;
using CodeZero.PlugIns;

namespace CodeZero
{
    public class CodeZeroBootstrapperOptions
    {
        /// <summary>
        /// Used to disable all interceptors added by CodeZero.
        /// </summary>
        public bool DisableAllInterceptors { get; set; }

        /// <summary>
        /// IIocManager that is used to bootstrap the CodeZero system. If set to null, uses global <see cref="CodeZero.Dependency.IocManager.Instance"/>
        /// </summary>
        public IIocManager IocManager { get; set; }

        /// <summary>
        /// List of plugin sources.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        public CodeZeroBootstrapperOptions()
        {
            IocManager = CodeZero.Dependency.IocManager.Instance;
            PlugInSources = new PlugInSourceList();
        }
    }
}
