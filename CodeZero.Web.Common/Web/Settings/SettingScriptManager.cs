//  <copyright file="SettingScriptManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Text;
using System.Threading.Tasks;
using CodeZero.Configuration;
using CodeZero.Dependency;
using CodeZero.Runtime.Session;
using CodeZero.Web.Http;

namespace CodeZero.Web.Settings
{
    /// <summary>
    /// This class is used to build setting script.
    /// </summary>
    public class SettingScriptManager : ISettingScriptManager, ISingletonDependency
    {
        private readonly ISettingDefinitionManager _settingDefinitionManager;
        private readonly ISettingManager _settingManager;
        private readonly ICodeZeroSession _CodeZeroSession;
        private readonly IIocResolver _iocResolver;

        public SettingScriptManager(
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            ICodeZeroSession CodeZeroSession,
            IIocResolver iocResolver)
        {
            _settingDefinitionManager = settingDefinitionManager;
            _settingManager = settingManager;
            _CodeZeroSession = CodeZeroSession;
            _iocResolver = iocResolver;
        }

        public async Task<string> GetScriptAsync()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine("    CodeZero.setting = CodeZero.setting || {};");
            script.AppendLine("    CodeZero.setting.values = {");

            var settingDefinitions = _settingDefinitionManager
                .GetAllSettingDefinitions();

            var added = 0;

            using (var scope = _iocResolver.CreateScope())
            {
                foreach (var settingDefinition in settingDefinitions)
                {
                    if (!await settingDefinition.ClientVisibilityProvider.CheckVisible(scope))
                    {
                        continue;
                    }

                    if (added > 0)
                    {
                        script.AppendLine(",");
                    }
                    else
                    {
                        script.AppendLine();
                    }

                    var settingValue = await _settingManager.GetSettingValueAsync(settingDefinition.Name);

                    script.Append("        '" +
                                  settingDefinition.Name.Replace("'", @"\'") + "': " +
                                  (settingValue == null ? "null" : "'" + HttpEncode.JavaScriptStringEncode(settingValue) + "'"));

                    ++added;
                }
            }

            script.AppendLine();
            script.AppendLine("    };");

            script.AppendLine();
            script.Append("})();");

            return script.ToString();
        }
    }
}