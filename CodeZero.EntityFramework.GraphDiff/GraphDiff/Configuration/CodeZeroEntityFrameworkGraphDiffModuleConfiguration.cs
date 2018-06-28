//  <copyright file="CodeZeroEntityFrameworkGraphDiffModuleConfiguration.cs" project="CodeZero.EntityFramework.GraphDiff" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.EntityFramework.GraphDiff.Mapping;

namespace CodeZero.EntityFramework.GraphDiff.Configuration
{
    public class CodeZeroEntityFrameworkGraphDiffModuleConfiguration : ICodeZeroEntityFrameworkGraphDiffModuleConfiguration
    {
        public List<EntityMapping> EntityMappings { get; set; }
    }
}