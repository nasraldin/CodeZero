//  <copyright file="MemoryRepositoryOfTEntityAndTPrimaryKey.cs" project="CodeZero.MemoryDb" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Linq;
using CodeZero.Domain.Entities;
using CodeZero.Domain.Repositories;

namespace CodeZero.MemoryDb.Repositories
{
    //TODO: Implement thread-safety..?
    public class MemoryRepository<TEntity, TPrimaryKey> : CodeZeroRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public virtual MemoryDatabase Database { get { return _databaseProvider.Database; } }

        public virtual List<TEntity> Table { get { return Database.Set<TEntity>(); } }

        private readonly IMemoryDatabaseProvider _databaseProvider;
        private readonly MemoryPrimaryKeyGenerator<TPrimaryKey> _primaryKeyGenerator;

        public MemoryRepository(IMemoryDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
            _primaryKeyGenerator = new MemoryPrimaryKeyGenerator<TPrimaryKey>();
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        public override TEntity Insert(TEntity entity)
        {
            if (entity.IsTransient())
            {
                entity.Id = _primaryKeyGenerator.GetNext();
            }

            Table.Add(entity);
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, entity.Id));
            if (index >= 0)
            {
                Table[index] = entity;
            }

            return entity;
        }

        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public override void Delete(TPrimaryKey id)
        {
            var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, id));
            if (index >= 0)
            {
                Table.RemoveAt(index);
            }
        }
    }
}