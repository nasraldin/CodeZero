//  <copyright file="ISettingScriptManager.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;

namespace CodeZero.Web.Settings
{
    /// <summary>
    /// Define interface to get setting scripts
    /// </summary>
    public interface ISettingScriptManager
    {
        /// <summary>
        /// Gets JavaScript that contains setting values.
        /// </summary>
        Task<string> GetScriptAsync();
    }
}