//  <copyright file="IFeatureDependency.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Defines a feature dependency.
    /// </summary>
    public interface IFeatureDependency
    {
        /// <summary>
        /// Checks dependent features and returns true if the dependencies are satisfied.
        /// </summary>
        Task<bool> IsSatisfiedAsync(IFeatureDependencyContext context);
    }
}