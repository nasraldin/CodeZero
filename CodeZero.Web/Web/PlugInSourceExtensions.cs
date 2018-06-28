//  <copyright file="PlugInSourceExtensions.cs" project="CodeZero.Web" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Web.Compilation;
using CodeZero.Logging;
using CodeZero.PlugIns;

namespace CodeZero.Web
{
    public static class PlugInSourceListExtensions
    {
        public static void AddToBuildManager(this PlugInSourceList plugInSourceList)
        {
            foreach (var plugInAssembly in plugInSourceList.GetAllAssemblies())
            {
                try
                {
                    LogHelper.Logger.Debug($"Adding {plugInAssembly.FullName} to BuildManager");
                    BuildManager.AddReferencedAssembly(plugInAssembly);
                }
                catch (Exception ex)
                {
                    LogHelper.Logger.Warn(ex.ToString(), ex);
                }
            }
        }
    }
}
