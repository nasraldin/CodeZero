//  <copyright file="IExtendableObject.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using JetBrains.Annotations;

namespace CodeZero.Domain.Entities
{
    /// <summary>
    /// Defines a JSON formatted string property to extend an object/entity.
    /// </summary>
    public interface IExtendableObject
    {
        /// <summary>
        /// A JSON formatted string to extend the containing object.
        /// JSON data can contain properties with arbitrary values (like primitives or complex objects).
        /// Extension methods are available (<see cref="ExtendableObjectExtensions"/>) to manipulate this data.
        /// General format:
        /// <code>
        /// {
        ///   "Property1" : ...
        ///   "Property2" : ...
        /// }
        /// </code>
        /// </summary>
        [CanBeNull]
        string ExtensionData { get; set; }
    }
}
