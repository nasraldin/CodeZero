//  <copyright file="NavigationScriptManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Text;
using System.Threading.Tasks;
using CodeZero.Application.Navigation;
using CodeZero.Dependency;
using CodeZero.Json;
using CodeZero.Runtime.Session;

namespace CodeZero.Web.Navigation
{
    internal class NavigationScriptManager : INavigationScriptManager, ITransientDependency
    {
        public ICodeZeroSession CodeZeroSession { get; set; }

        private readonly IUserNavigationManager _userNavigationManager;

        public NavigationScriptManager(IUserNavigationManager userNavigationManager)
        {
            _userNavigationManager = userNavigationManager;
            CodeZeroSession = NullCodeZeroSession.Instance;
        }

        public async Task<string> GetScriptAsync()
        {
            var userMenus = await _userNavigationManager.GetMenusAsync(CodeZeroSession.ToUserIdentifier());

            var sb = new StringBuilder();
            sb.AppendLine("(function() {");

            sb.AppendLine("    CodeZero.nav = {};");
            sb.AppendLine("    CodeZero.nav.menus = {");

            for (int i = 0; i < userMenus.Count; i++)
            {
                AppendMenu(sb, userMenus[i]);
                if (userMenus.Count - 1 > i)
                {
                    sb.Append(" , ");
                }
            }

            sb.AppendLine("    };");

            sb.AppendLine("})();");

            return sb.ToString();
        }

        private static void AppendMenu(StringBuilder sb, UserMenu menu)
        {
            sb.AppendLine("        '" + menu.Name + "': {");

            sb.AppendLine("            name: '" + menu.Name + "',");

            if (menu.DisplayName != null)
            {
                sb.AppendLine("            displayName: '" + menu.DisplayName + "',");
            }

            if (menu.CustomData != null)
            {
                sb.AppendLine("            customData: " + menu.CustomData.ToJsonString(true) + ",");
            }

            sb.Append("            items: ");

            if (menu.Items.Count <= 0)
            {
                sb.AppendLine("[]");
            }
            else
            {
                sb.Append("[");
                for (int i = 0; i < menu.Items.Count; i++)
                {
                    AppendMenuItem(16, sb, menu.Items[i]);
                    if (menu.Items.Count - 1 > i)
                    {
                        sb.Append(" , ");
                    }
                }
                sb.AppendLine("]");
            }

            sb.AppendLine("            }");
        }

        private static void AppendMenuItem(int indentLength, StringBuilder sb, UserMenuItem menuItem)
        {
            sb.AppendLine("{");

            sb.AppendLine(new string(' ', indentLength + 4) + "name: '" + menuItem.Name + "',");
            sb.AppendLine(new string(' ', indentLength + 4) + "order: " + menuItem.Order + ",");

            if (!string.IsNullOrEmpty(menuItem.Icon))
            {
                sb.AppendLine(new string(' ', indentLength + 4) + "icon: '" + menuItem.Icon.Replace("'", @"\'") + "',");
            }

            if (!string.IsNullOrEmpty(menuItem.Url))
            {
                sb.AppendLine(new string(' ', indentLength + 4) + "url: '" + menuItem.Url.Replace("'", @"\'") + "',");
            }

            if (menuItem.DisplayName != null)
            {
                sb.AppendLine(new string(' ', indentLength + 4) + "displayName: '" + menuItem.DisplayName.Replace("'", @"\'") + "',");
            }

            if (menuItem.CustomData != null)
            {
                sb.AppendLine(new string(' ', indentLength + 4) + "customData: " + menuItem.CustomData.ToJsonString(true) + ",");
            }

            if (menuItem.Target != null)
            {
                sb.AppendLine(new string(' ', indentLength + 4) + "target: '" + menuItem.Target.Replace("'", @"\'") + "',");
            }

            sb.AppendLine(new string(' ', indentLength + 4) + "isEnabled: " + menuItem.IsEnabled.ToString().ToLowerInvariant() + ",");
            sb.AppendLine(new string(' ', indentLength + 4) + "isVisible: " + menuItem.IsVisible.ToString().ToLowerInvariant() + ",");

            sb.Append(new string(' ', indentLength + 4) + "items: [");

            for (int i = 0; i < menuItem.Items.Count; i++)
            {
                AppendMenuItem(24, sb, menuItem.Items[i]);
                if (menuItem.Items.Count - 1 > i)
                {
                    sb.Append(" , ");
                }
            }

            sb.AppendLine("]");

            sb.Append(new string(' ', indentLength) + "}");
        }
    }
}