//  <copyright file="IMultiTenancyScriptManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Web.MultiTenancy
{
    /// <summary>
    /// Used to create client scripts for multi-tenancy.
    /// </summary>
    public interface IMultiTenancyScriptManager
    {
        string GetScript();
    }
}