//  <copyright file="EntityHistoryStore.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Dependency;
using CodeZero.Domain.Repositories;

namespace CodeZero.EntityHistory
{
    /// <summary>
    /// Implements <see cref="IEntityHistoryStore"/> to save entity change informations to database.
    /// </summary>
    public class EntityHistoryStore : IEntityHistoryStore, ITransientDependency
    {
        private readonly IRepository<EntityChangeSet, long> _changeSetRepository;

        /// <summary>
        /// Creates a new <see cref="EntityHistoryStore"/>.
        /// </summary>
        public EntityHistoryStore(IRepository<EntityChangeSet, long> changeSetRepository)
        {
            _changeSetRepository = changeSetRepository;
        }

        public virtual Task SaveAsync(EntityChangeSet changeSet)
        {
            return _changeSetRepository.InsertAsync(changeSet);
        }
    }
}
