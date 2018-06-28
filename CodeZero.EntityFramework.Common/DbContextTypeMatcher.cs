//  <copyright file="DbContextTypeMatcher.cs" project="CodeZero.EntityFramework.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeZero.Collections.Extensions;
using CodeZero.Dependency;
using CodeZero.Domain.Repositories;
using CodeZero.Domain.Uow;
using CodeZero.MultiTenancy;

namespace CodeZero.EntityFramework
{
    public abstract class DbContextTypeMatcher<TBaseDbContext> : IDbContextTypeMatcher, ISingletonDependency
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        private readonly Dictionary<Type, List<Type>> _dbContextTypes;

        protected DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _dbContextTypes = new Dictionary<Type, List<Type>>();
        }

        public void Populate(Type[] dbContextTypes)
        {
            foreach (var dbContextType in dbContextTypes)
            {
                var types = new List<Type>();

                AddWithBaseTypes(dbContextType, types);

                foreach (var type in types)
                {
                    Add(type, dbContextType);
                }
            }
        }

        //TODO: GetConcreteType method can be optimized by extracting/caching MultiTenancySideAttribute attributes for DbContexes.

        public virtual Type GetConcreteType(Type sourceDbContextType)
        {
            //TODO: This can also get MultiTenancySide to filter dbcontexes

            if (!sourceDbContextType.GetTypeInfo().IsAbstract)
            {
                return sourceDbContextType;
            }
            
            //Get possible concrete types for given DbContext type
            var allTargetTypes = _dbContextTypes.GetOrDefault(sourceDbContextType);

            if (allTargetTypes.IsNullOrEmpty())
            {
                throw new CodeZeroException("Could not find a concrete implementation of given DbContext type: " + sourceDbContextType.AssemblyQualifiedName);
            }

            if (allTargetTypes.Count == 1)
            {
                //Only one type does exists, return it
                return allTargetTypes[0];
            }

            CheckCurrentUow();

            var currentTenancySide = GetCurrentTenancySide();

            var multiTenancySideContexes = GetMultiTenancySideContextTypes(allTargetTypes, currentTenancySide);

            if (multiTenancySideContexes.Count == 1)
            {
                return multiTenancySideContexes[0];
            }

            if (multiTenancySideContexes.Count > 1)
            {
                return GetDefaultDbContextType(multiTenancySideContexes, sourceDbContextType, currentTenancySide);
            }

            return GetDefaultDbContextType(allTargetTypes, sourceDbContextType, currentTenancySide);
        }

        private void CheckCurrentUow()
        {
            if (_currentUnitOfWorkProvider.Current == null)
            {
                throw new CodeZeroException("GetConcreteType method should be called in a UOW.");
            }
        }

        private MultiTenancySides GetCurrentTenancySide()
        {
            return _currentUnitOfWorkProvider.Current.GetTenantId() == null
                       ? MultiTenancySides.Host
                       : MultiTenancySides.Tenant;
        }

        private static List<Type> GetMultiTenancySideContextTypes(List<Type> dbContextTypes, MultiTenancySides tenancySide)
        {
            return dbContextTypes.Where(type =>
            {
                var attrs = type.GetTypeInfo().GetCustomAttributes(typeof(MultiTenancySideAttribute), true).ToArray();
                if (attrs.IsNullOrEmpty())
                {
                    return false;
                }

                return ((MultiTenancySideAttribute)attrs[0]).Side.HasFlag(tenancySide);
            }).ToList();
        }

        private static Type GetDefaultDbContextType(List<Type> dbContextTypes, Type sourceDbContextType, MultiTenancySides tenancySide)
        {
            var filteredTypes = dbContextTypes
                .Where(type => !type.GetTypeInfo().IsDefined(typeof(AutoRepositoryTypesAttribute), true))
                .ToList();

            if (filteredTypes.Count == 1)
            {
                return filteredTypes[0];
            }

            filteredTypes = filteredTypes
                .Where(type => type.GetTypeInfo().IsDefined(typeof(DefaultDbContextAttribute), true))
                .ToList();

            if (filteredTypes.Count == 1)
            {
                return filteredTypes[0];
            }

            throw new CodeZeroException(string.Format(
                "Found more than one concrete type for given DbContext Type ({0}) define MultiTenancySideAttribute with {1}. Found types: {2}.",
                sourceDbContextType,
                tenancySide,
                dbContextTypes.Select(c => c.AssemblyQualifiedName).JoinAsString(", ")
                ));
        }

        private static void AddWithBaseTypes(Type dbContextType, List<Type> types)
        {
            types.Add(dbContextType);
            if (dbContextType != typeof(TBaseDbContext))
            {
                AddWithBaseTypes(dbContextType.GetTypeInfo().BaseType, types);
            }
        }

        private void Add(Type sourceDbContextType, Type targetDbContextType)
        {
            if (!_dbContextTypes.ContainsKey(sourceDbContextType))
            {
                _dbContextTypes[sourceDbContextType] = new List<Type>();
            }

            _dbContextTypes[sourceDbContextType].Add(targetDbContextType);
        }
    }
}