//  <copyright file="AndSpecification.cs" project="CodeZero" solution="CodeZero">
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
    /// Represents the combined specification which indicates that both of the given
    /// specifications should be satisfied by the given object.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        /// <summary>
        /// Constructs a new instance of <see cref="AndSpecification{T}"/> class.
        /// </summary>
        /// <param name="left">The first specification.</param>
        /// <param name="right">The second specification.</param>
        public AndSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }

        /// <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public override Expression<Func<T, bool>> ToExpression()
        {
            return Left.ToExpression().And(Right.ToExpression());
        }
    }
}
