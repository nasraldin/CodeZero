//  <copyright file="CodeZeroUserConfigurationDto.cs" project="CodeZero.Web.Common" solution="CodeZero">
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
    public class CodeZeroUserConfigurationDto
    {
        public CodeZeroMultiTenancyConfigDto MultiTenancy { get; set; }

        public CodeZeroUserSessionConfigDto Session { get; set; }

        public CodeZeroUserLocalizationConfigDto Localization { get; set; }

        public CodeZeroUserFeatureConfigDto Features { get; set; }

        public CodeZeroUserAuthConfigDto Auth { get; set; }

        public CodeZeroUserNavConfigDto Nav { get; set; }

        public CodeZeroUserSettingConfigDto Setting { get; set; }

        public CodeZeroUserClockConfigDto Clock { get; set; }

        public CodeZeroUserTimingConfigDto Timing { get; set; }

        public CodeZeroUserSecurityConfigDto Security { get; set; }

        public Dictionary<string, object> Custom { get; set; }
    }
}