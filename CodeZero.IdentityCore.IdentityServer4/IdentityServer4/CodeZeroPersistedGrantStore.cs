//  <copyright file="CodeZeroPersistedGrantStore.cs" project="CodeZero.IdentityCore.IdentityServer4" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeZero.Domain.Repositories;
using CodeZero.Domain.Uow;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace CodeZero.IdentityServer4
{
    public class CodeZeroPersistedGrantStore : CodeZeroServiceBase, IPersistedGrantStore
    {
        private readonly IRepository<PersistedGrantEntity, string> _persistedGrantRepository;

        public CodeZeroPersistedGrantStore(IRepository<PersistedGrantEntity, string> persistedGrantRepository)
        {
            _persistedGrantRepository = persistedGrantRepository;
        }

        [UnitOfWork]
        public virtual async Task StoreAsync(PersistedGrant grant)
        {
            var entity = await _persistedGrantRepository.FirstOrDefaultAsync(grant.Key);
            if (entity == null)
            {
                await _persistedGrantRepository.InsertAsync(ObjectMapper.Map<PersistedGrantEntity>(grant));
            }
            else
            {
                ObjectMapper.Map(grant, entity);
            }
        }

        [UnitOfWork]
        public virtual async Task<PersistedGrant> GetAsync(string key)
        {
            var entity = await _persistedGrantRepository.FirstOrDefaultAsync(key);
            if (entity == null)
            {
                return null;
            }

            return ObjectMapper.Map<PersistedGrant>(entity);
        }

        [UnitOfWork]
        public virtual async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            var entities = await _persistedGrantRepository.GetAllListAsync(x => x.SubjectId == subjectId);
            return ObjectMapper.Map<List<PersistedGrant>>(entities);
        }

        [UnitOfWork]
        public virtual async Task RemoveAsync(string key)
        {
            await _persistedGrantRepository.DeleteAsync(key);
        }

        [UnitOfWork]
        public virtual async Task RemoveAllAsync(string subjectId, string clientId)
        {
            await _persistedGrantRepository.DeleteAsync(x => x.SubjectId == subjectId && x.ClientId == clientId);
        }

        [UnitOfWork]
        public virtual async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            await _persistedGrantRepository.DeleteAsync(x => x.SubjectId == subjectId && x.ClientId == clientId && x.Type == type);
        }
    }
}
