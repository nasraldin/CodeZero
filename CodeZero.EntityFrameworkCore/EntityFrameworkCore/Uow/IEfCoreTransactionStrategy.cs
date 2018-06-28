//  <copyright file="IEfCoreTransactionStrategy.cs" project="CodeZero.EntityFrameworkCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;
using CodeZero.Domain.Uow;
using Microsoft.EntityFrameworkCore;

namespace CodeZero.EntityFrameworkCore.Uow
{
  public interface IEfCoreTransactionStrategy
  {
    void InitOptions(UnitOfWorkOptions options);

    DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver)
        where TDbContext : DbContext;

    void Commit();

    void Dispose(IIocResolver iocResolver);
  }
}
