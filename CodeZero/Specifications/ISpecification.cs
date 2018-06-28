//  <copyright file="ISpecification.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq.Expressions;

namespace CodeZero.Specifications
{
    /// <summary>
    /// Represents that the implemented classes are specifications. For more
    /// information about the specification pattern, please refer to
    /// http://martinfowler.com/apsupp/spec.pdf.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Returns a <see cref="bool"/> value which indicates whether the specification
        /// is satisfied by the given object.
        /// </summary>
        /// <param name="obj">The object to which the specification is applied.</param>
        /// <returns>True if the specification is satisfied, otherwise false.</returns>
        bool IsSatisfiedBy(T obj);

        /// <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        Expression<Func<T, bool>> ToExpression();
    }
}
