//  <copyright file="ICodeZeroControllerAssemblySettingBuilder.cs" project="CodeZero.AspNetCore" solution="CodeZero">
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
    public interface ICodeZeroControllerAssemblySettingBuilder
    {
        CodeZeroControllerAssemblySettingBuilder Where(Func<Type, bool> predicate);

        CodeZeroControllerAssemblySettingBuilder ConfigureControllerModel(Action<ControllerModel> configurer);
    }
}