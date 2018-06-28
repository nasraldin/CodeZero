//  <copyright file="IWebEmbeddedResourcesConfiguration.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;

namespace CodeZero.Web.Configuration
{
    public interface IWebEmbeddedResourcesConfiguration
    {
        /// <summary>
        /// List of file extensions (without dot) to ignore for embedded resources.
        /// Default extensions: cshtml, config.
        /// </summary>
        HashSet<string> IgnoredFileExtensions { get; }
    }
}