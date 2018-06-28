//  <copyright file="IFeatureManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Feature manager.
    /// </summary>
    public interface IFeatureManager
    {
        /// <summary>
        /// Gets the <see cref="Feature"/> by a specified name.
        /// </summary>
        /// <param name="name">Unique name of the feature.</param>
        Feature Get(string name);

        /// <summary>
        /// Gets the <see cref="Feature"/> by a specified name or returns null if it can not be found.
        /// </summary>
        /// <param name="name">The name.</param>
        Feature GetOrNull(string name);

        /// <summary>
        /// Gets all <see cref="Feature"/>s.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<Feature> GetAll();
    }
}