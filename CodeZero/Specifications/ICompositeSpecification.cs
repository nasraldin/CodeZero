//  <copyright file="ICompositeSpecification.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Specifications
{
    /// <summary>
    /// Represents that the implemented classes are composite specifications.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public interface ICompositeSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Gets the left side of the specification.
        /// </summary>
        ISpecification<T> Left { get; }

        /// <summary>
        /// Gets the right side of the specification.
        /// </summary>
        ISpecification<T> Right { get; }
    }
}
