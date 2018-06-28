//  <copyright file="CodeZeroControllerAssemblySetting.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CodeZero.AspNetCore.Configuration
{
    public class CodeZeroControllerAssemblySetting
    {
        /// <summary>
        /// "app".
        /// </summary>
        public const string DefaultServiceModuleName = "app";

        public string ModuleName { get; }

        public Assembly Assembly { get; }

        public bool UseConventionalHttpVerbs { get; }

        public Func<Type, bool> TypePredicate { get; set; }

        public Action<ControllerModel> ControllerModelConfigurer { get; set; }

        public CodeZeroControllerAssemblySetting(string moduleName, Assembly assembly, bool useConventionalHttpVerbs)
        {
            ModuleName = moduleName;
            Assembly = assembly;
            UseConventionalHttpVerbs = useConventionalHttpVerbs;

            TypePredicate = type => true;
            ControllerModelConfigurer = controller => { };
        }
    }
}