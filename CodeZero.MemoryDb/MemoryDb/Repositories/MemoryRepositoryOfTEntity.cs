//  <copyright file="MemoryRepositoryOfTEntity.cs" project="CodeZero.MemoryDb" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Domain.Entities;
using CodeZero.Domain.Repositories;

namespace CodeZero.MemoryDb.Repositories
{
    public class MemoryRepository<TEntity> : MemoryRepository<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public MemoryRepository(IMemoryDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }
}
