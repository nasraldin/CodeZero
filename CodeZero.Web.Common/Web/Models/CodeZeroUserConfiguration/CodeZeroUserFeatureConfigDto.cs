//  <copyright file="CodeZeroUserFeatureConfigDto.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;

namespace CodeZero.Web.Models.CodeZeroUserConfiguration
{
    public class CodeZeroUserFeatureConfigDto
    {
        public Dictionary<string, CodeZeroStringValueDto> AllFeatures { get; set; }
    }
}