//  <copyright file="ControllerAssemblySettingList.cs" project="CodeZero.AspNetCore" solution="CodeZero">
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
using CodeZero.Reflection.Extensions;
using JetBrains.Annotations;

namespace CodeZero.AspNetCore.Configuration
{
    public class ControllerAssemblySettingList : List<CodeZeroControllerAssemblySetting>
    {
        [CanBeNull]
        public CodeZeroControllerAssemblySetting GetSettingOrNull(Type controllerType)
        {
            return this.FirstOrDefault(controllerSetting => controllerSetting.Assembly == controllerType.GetAssembly());
        }
    }
}