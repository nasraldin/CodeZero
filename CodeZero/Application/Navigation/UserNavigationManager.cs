//  <copyright file="UserNavigationManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeZero.Application.Features;
using CodeZero.Authorization;
using CodeZero.Collections.Extensions;
using CodeZero.Dependency;
using CodeZero.Localization;
using CodeZero.MultiTenancy;
using CodeZero.Runtime.Session;

namespace CodeZero.Application.Navigation
{
    internal class UserNavigationManager : IUserNavigationManager, ITransientDependency
    {
        public ICodeZeroSession CodeZeroSession { get; set; }

        private readonly INavigationManager _navigationManager;
        private readonly ILocalizationContext _localizationContext;
        private readonly IIocResolver _iocResolver;

        public UserNavigationManager(
            INavigationManager navigationManager,
            ILocalizationContext localizationContext,
            IIocResolver iocResolver)
        {
            _navigationManager = navigationManager;
            _localizationContext = localizationContext;
            _iocResolver = iocResolver;
            CodeZeroSession = NullCodeZeroSession.Instance;
        }

        public async Task<UserMenu> GetMenuAsync(string menuName, UserIdentifier user)
        {
            var menuDefinition = _navigationManager.Menus.GetOrDefault(menuName);
            if (menuDefinition == null)
            {
                throw new CodeZeroException("There is no menu with given name: " + menuName);
            }

            var userMenu = new UserMenu(menuDefinition, _localizationContext);
            await FillUserMenuItems(user, menuDefinition.Items, userMenu.Items);
            return userMenu;
        }

        public async Task<IReadOnlyList<UserMenu>> GetMenusAsync(UserIdentifier user)
        {
            var userMenus = new List<UserMenu>();

            foreach (var menu in _navigationManager.Menus.Values)
            {
                userMenus.Add(await GetMenuAsync(menu.Name, user));
            }

            return userMenus;
        }

        private async Task<int> FillUserMenuItems(UserIdentifier user, IList<MenuItemDefinition> menuItemDefinitions, IList<UserMenuItem> userMenuItems)
        {
            //TODO: Can be optimized by re-using FeatureDependencyContext.

            var addedMenuItemCount = 0;

            using (var scope = _iocResolver.CreateScope())
            {
                var permissionDependencyContext = scope.Resolve<PermissionDependencyContext>();
                permissionDependencyContext.User = user;

                var featureDependencyContext = scope.Resolve<FeatureDependencyContext>();
                featureDependencyContext.TenantId = user == null ? null : user.TenantId;
                
                foreach (var menuItemDefinition in menuItemDefinitions)
                {
                    if (menuItemDefinition.RequiresAuthentication && user == null)
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(menuItemDefinition.RequiredPermissionName))
                    {
                        var permissionDependency = new SimplePermissionDependency(menuItemDefinition.RequiredPermissionName);
                        if (user == null || !(await permissionDependency.IsSatisfiedAsync(permissionDependencyContext)))
                        {
                            continue;
                        }
                    }

                    if (menuItemDefinition.PermissionDependency != null &&
                        (user == null || !(await menuItemDefinition.PermissionDependency.IsSatisfiedAsync(permissionDependencyContext))))
                    {
                        continue;
                    }

                    if (menuItemDefinition.FeatureDependency != null &&
                        (CodeZeroSession.MultiTenancySide == MultiTenancySides.Tenant || (user != null && user.TenantId != null)) &&
                        !(await menuItemDefinition.FeatureDependency.IsSatisfiedAsync(featureDependencyContext)))
                    {
                        continue;
                    }

                    var userMenuItem = new UserMenuItem(menuItemDefinition, _localizationContext);
                    if (menuItemDefinition.IsLeaf || (await FillUserMenuItems(user, menuItemDefinition.Items, userMenuItem.Items)) > 0)
                    {
                        userMenuItems.Add(userMenuItem);
                        ++addedMenuItemCount;
                    }
                }
            }

            return addedMenuItemCount;
        }
    }
}
