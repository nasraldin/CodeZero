//  <copyright file="MappingExpressionBuilder.cs" project="CodeZero.EntityFramework.GraphDiff" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq.Expressions;
using CodeZero.Domain.Entities;
using RefactorThis.GraphDiff;

namespace CodeZero.EntityFramework.GraphDiff.Mapping
{
    /// <summary>
    /// Helper class for creating entity mappings
    /// </summary>
    public static class MappingExpressionBuilder
    {
        /// <summary>
        /// A shortcut of <see cref="For{TEntity,TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static EntityMapping For<TEntity>(Expression<Func<IUpdateConfiguration<TEntity>, object>> expression)
            where TEntity : class, IEntity
        {
            return For<TEntity, int>(expression);
        }

        /// <summary>
        /// Build a mapping for an entity with a specified primary key
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static EntityMapping For<TEntity, TPrimaryKey>(Expression<Func<IUpdateConfiguration<TEntity>, object>> expression)
            where TPrimaryKey : IEquatable<TPrimaryKey>
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return new EntityMapping(typeof(TEntity), expression);
        }
    }
}
