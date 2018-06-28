//  <copyright file="CodeZeroWebModuleConfiguration.cs" project="CodeZero.Web" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Web.Security.AntiForgery;

namespace CodeZero.Web.Configuration
{
    public class CodeZeroWebModuleConfiguration : ICodeZeroWebModuleConfiguration
    {
        public ICodeZeroAntiForgeryWebConfiguration AntiForgery { get; }
        public ICodeZeroWebLocalizationConfiguration Localization { get; }

        public CodeZeroWebModuleConfiguration(
            ICodeZeroAntiForgeryWebConfiguration antiForgery, 
            ICodeZeroWebLocalizationConfiguration localization)
        {
            AntiForgery = antiForgery;
            Localization = localization;
        }
    }
}