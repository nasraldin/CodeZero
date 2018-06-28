//  <copyright file="LocalizationScriptManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Globalization;
using System.Linq;
using System.Text;
using CodeZero.Dependency;
using CodeZero.Json;
using CodeZero.Localization;

namespace CodeZero.Web.Localization
{
    internal class LocalizationScriptManager : ILocalizationScriptManager, ISingletonDependency
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly ILanguageManager _languageManager;

        public LocalizationScriptManager(
            ILocalizationManager localizationManager,
            ILanguageManager languageManager)
        {
            _localizationManager = localizationManager;
            _languageManager = languageManager;
        }

        /// <inheritdoc/>
        public string GetScript()
        {
            return GetScript(CultureInfo.CurrentUICulture);
        }

        /// <inheritdoc/>
        public string GetScript(CultureInfo cultureInfo)
        {
            //NOTE: Disabled caching since it's not true (localization script is changed per user, per tenant, per culture...)
            return BuildAll(cultureInfo);
            //return _cacheManager.GetCache(CodeZeroCacheNames.LocalizationScripts).Get(cultureInfo.Name, () => BuildAll(cultureInfo));
        }

        private string BuildAll(CultureInfo cultureInfo)
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine();
            script.AppendLine("    CodeZero.localization = CodeZero.localization || {};");
            script.AppendLine();
            script.AppendLine("    CodeZero.localization.currentCulture = {");
            script.AppendLine("        name: '" + cultureInfo.Name + "',");
            script.AppendLine("        displayName: '" + cultureInfo.DisplayName + "'");
            script.AppendLine("    };");
            script.AppendLine();
            script.Append("    CodeZero.localization.languages = [");

            var languages = _languageManager.GetLanguages();
            for (var i = 0; i < languages.Count; i++)
            {
                var language = languages[i];

                script.AppendLine("{");
                script.AppendLine("        name: '" + language.Name + "',");
                script.AppendLine("        displayName: '" + language.DisplayName + "',");
                script.AppendLine("        icon: '" + language.Icon + "',");
                script.AppendLine("        isDisabled: " + language.IsDisabled.ToString().ToLowerInvariant() + ",");
                script.AppendLine("        isDefault: " + language.IsDefault.ToString().ToLowerInvariant());
                script.Append("    }");

                if (i < languages.Count - 1)
                {
                    script.Append(" , ");
                }
            }

            script.AppendLine("];");
            script.AppendLine();

            if (languages.Count > 0)
            {
                var currentLanguage = _languageManager.CurrentLanguage;
                script.AppendLine("    CodeZero.localization.currentLanguage = {");
                script.AppendLine("        name: '" + currentLanguage.Name + "',");
                script.AppendLine("        displayName: '" + currentLanguage.DisplayName + "',");
                script.AppendLine("        icon: '" + currentLanguage.Icon + "',");
                script.AppendLine("        isDisabled: " + currentLanguage.IsDisabled.ToString().ToLowerInvariant() + ",");
                script.AppendLine("        isDefault: " + currentLanguage.IsDefault.ToString().ToLowerInvariant());
                script.AppendLine("    };");
            }

            var sources = _localizationManager.GetAllSources().OrderBy(s => s.Name).ToArray();

            script.AppendLine();
            script.AppendLine("    CodeZero.localization.sources = [");

            for (int i = 0; i < sources.Length; i++)
            {
                var source = sources[i];
                script.AppendLine("        {");
                script.AppendLine("            name: '" + source.Name + "',");
                script.AppendLine("            type: '" + source.GetType().Name + "'");
                script.AppendLine("        }" + (i < (sources.Length - 1) ? "," : ""));
            }

            script.AppendLine("    ];");

            script.AppendLine();
            script.AppendLine("    CodeZero.localization.values = CodeZero.localization.values || {};");
            script.AppendLine();

            foreach (var source in sources)
            {
                script.Append("    CodeZero.localization.values['" + source.Name + "'] = ");

                var stringValues = source.GetAllStrings(cultureInfo).OrderBy(s => s.Name).ToList();
                var stringJson = stringValues
                    .ToDictionary(_ => _.Name, _ => _.Value)
                    .ToJsonString(indented: true);
                script.Append(stringJson);

                script.AppendLine(";");
                script.AppendLine();
            }

            script.AppendLine();
            script.Append("})();");

            return script.ToString();
        }
    }
}
