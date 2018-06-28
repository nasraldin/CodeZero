//  <copyright file="IFeatureChecker.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Runtime.Session;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// This interface should be used to get the value of features
    /// </summary>
    public interface IFeatureChecker
    {
        /// <summary>
        /// Gets the value of a feature by its name.
        /// This is a shortcut for <see cref="GetValueAsync(int, string)"/> that uses <see cref="ICodeZeroSession.TenantId"/> as tenantId.
        /// Note: This method should only be used if a TenantId can be obtained from the session.
        /// </summary>
        /// <param name="name">Unique feature name</param>
        /// <returns>Feature's current value</returns>
        Task<string> GetValueAsync(string name);

        /// <summary>
        /// Gets the value of a feature for a tenant by the feature's name.
        /// </summary>
        /// <param name="tenantId">Tenant's Id</param>
        /// <param name="name">Unique feature name</param>
        /// <returns>Feature's current value</returns>
        Task<string> GetValueAsync(int tenantId, string name);
    }
}