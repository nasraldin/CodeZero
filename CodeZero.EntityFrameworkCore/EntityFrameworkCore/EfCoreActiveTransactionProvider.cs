//  <copyright file="EfCoreActiveTransactionProvider.cs" project="CodeZero.EntityFrameworkCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Data;
using System.Reflection;
using CodeZero.Data;
using CodeZero.Dependency;
using CodeZero.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodeZero.EntityFrameworkCore
{
    public class EfCoreActiveTransactionProvider : IActiveTransactionProvider, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;

        public EfCoreActiveTransactionProvider(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IDbTransaction GetActiveTransaction(ActiveTransactionProviderArgs args)
        {
            return GetDbContext(args).Database.CurrentTransaction?.GetDbTransaction();
        }

        public IDbConnection GetActiveConnection(ActiveTransactionProviderArgs args)
        {
            return GetDbContext(args).Database.GetDbConnection();
        }

        private DbContext GetDbContext(ActiveTransactionProviderArgs args)
        {
            Type dbContextProviderType = typeof(IDbContextProvider<>).MakeGenericType((Type)args["ContextType"]);

            using (IDisposableDependencyObjectWrapper dbContextProviderWrapper = _iocResolver.ResolveAsDisposable(dbContextProviderType))
            {
                MethodInfo method = dbContextProviderWrapper.Object.GetType()
                                                            .GetMethod(
                                                                nameof(IDbContextProvider<CodeZeroDbContext>.GetDbContext),
                                                                new[] { typeof(MultiTenancySides) }
                                                            );

                return (DbContext)method.Invoke(
                    dbContextProviderWrapper.Object,
                    new object[] { (MultiTenancySides?)args["MultiTenancySide"] }
                );
            }
        }
    }
}