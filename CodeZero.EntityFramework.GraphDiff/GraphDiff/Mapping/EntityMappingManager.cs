//  <copyright file="EntityMappingManager.cs" project="CodeZero.EntityFramework.GraphDiff" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using System.Linq.Expressions;
using CodeZero.Dependency;
using CodeZero.EntityFramework.GraphDiff.Configuration;
using RefactorThis.GraphDiff;

namespace CodeZero.EntityFramework.GraphDiff.Mapping
{
    /// <summary>
    /// Used for resolving mappings for a GraphDiff repository extension methods
    /// </summary>
    public class EntityMappingManager : IEntityMappingManager, ITransientDependency
    {
        private readonly ICodeZeroEntityFrameworkGraphDiffModuleConfiguration _moduleConfiguration;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityMappingManager(ICodeZeroEntityFrameworkGraphDiffModuleConfiguration moduleConfiguration)
        {
            _moduleConfiguration = moduleConfiguration;
        }

        /// <summary>
        /// Gets an entity mapping or null for a specified entity type
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>Entity mapping or null if mapping doesn't exist</returns>
        public Expression<Func<IUpdateConfiguration<TEntity>, object>> GetEntityMappingOrNull<TEntity>()
        {
            var entityMapping = _moduleConfiguration.EntityMappings.FirstOrDefault(m => m.EntityType == typeof(TEntity));
            var mappingExptession = entityMapping?.MappingExpression as Expression<Func<IUpdateConfiguration<TEntity>, object>>;
            return mappingExptession;
        }
    }
}