//  <copyright file="CodeZeroControllerAssemblySettingBuilder.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CodeZero.AspNetCore.Configuration
{
    public class CodeZeroControllerAssemblySettingBuilder : ICodeZeroControllerAssemblySettingBuilder
    {
        private readonly CodeZeroControllerAssemblySetting _setting;

        public CodeZeroControllerAssemblySettingBuilder(CodeZeroControllerAssemblySetting setting)
        {
            _setting = setting;
        }

        public CodeZeroControllerAssemblySettingBuilder Where(Func<Type, bool> predicate)
        {
            _setting.TypePredicate = predicate;
            return this;
        }

        public CodeZeroControllerAssemblySettingBuilder ConfigureControllerModel(Action<ControllerModel> configurer)
        {
            _setting.ControllerModelConfigurer = configurer;
            return this;
        }
    }
}