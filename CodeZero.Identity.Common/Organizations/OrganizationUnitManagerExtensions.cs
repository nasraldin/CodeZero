//  <copyright file="OrganizationUnitManagerExtensions.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.Threading;

namespace CodeZero.Organizations
{
    public static class OrganizationUnitManagerExtensions
    {
        public static string GetCode(this OrganizationUnitManager manager, long id)
        {
            return AsyncHelper.RunSync(() => manager.GetCodeAsync(id));
        }

        public static void Create(this OrganizationUnitManager manager, OrganizationUnit organizationUnit)
        {
            AsyncHelper.RunSync(() => manager.CreateAsync(organizationUnit));
        }

        public static void Update(this OrganizationUnitManager manager, OrganizationUnit organizationUnit)
        {
            AsyncHelper.RunSync(() => manager.UpdateAsync(organizationUnit));
        }

        public static void Delete(this OrganizationUnitManager manager, long id)
        {
            AsyncHelper.RunSync(() => manager.DeleteAsync(id));
        }

        public static string GetNextChildCode(this OrganizationUnitManager manager, long? parentId)
        {
            return AsyncHelper.RunSync(() => manager.GetNextChildCodeAsync(parentId));
        }

        public static OrganizationUnit GetLastChildOrNull(this OrganizationUnitManager manager, long? parentId)
        {
            return AsyncHelper.RunSync(() => manager.GetLastChildOrNullAsync(parentId));
        }

        public static void Move(this OrganizationUnitManager manager, long id, long? parentId)
        {
            AsyncHelper.RunSync(() => manager.MoveAsync(id, parentId));
        }

        public static List<OrganizationUnit> FindChildren(this OrganizationUnitManager manager, long? parentId, bool recursive = false)
        {
            return AsyncHelper.RunSync(() => manager.FindChildrenAsync(parentId, recursive));
        }
    }
}