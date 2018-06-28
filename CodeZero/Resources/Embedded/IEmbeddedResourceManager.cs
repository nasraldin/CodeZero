//  <copyright file="IEmbeddedResourceManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using JetBrains.Annotations;
using System.Collections.Generic;

namespace CodeZero.Resources.Embedded
{
    /// <summary>
    /// Provides infrastructure to use any type of resources (files) embedded into assemblies.
    /// </summary>
    public interface IEmbeddedResourceManager
    {
        /// <summary>
        /// Used to get an embedded resource file.
        /// Can return null if resource is not found!
        /// </summary>
        /// <param name="fullResourcePath">Full path of the resource</param>
        /// <returns>The resource</returns>
        [CanBeNull]
        EmbeddedResourceItem GetResource([NotNull] string fullResourcePath);

        /// <summary>
        /// Used to get the list of embedded resource file.
        /// </summary>
        /// <param name="fullResourcePath">Full path of the resource</param>
        /// <returns>The list of resource</returns>
        IEnumerable<EmbeddedResourceItem> GetResources(string fullResourcePath);
    }
}