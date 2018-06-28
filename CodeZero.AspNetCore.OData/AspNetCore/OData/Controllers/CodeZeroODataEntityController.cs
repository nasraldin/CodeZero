//  <copyright file="CodeZeroODataEntityController.cs" project="CodeZero.AspNetCore.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CodeZero.Domain.Entities;
using CodeZero.Domain.Repositories;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CodeZero.AspNetCore.OData.Controllers
{
    public abstract class CodeZeroODataEntityController<TEntity> : CodeZeroODataEntityController<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected CodeZeroODataEntityController(IRepository<TEntity> repository)
            : base(repository)
        {

        }
    }

    public abstract class CodeZeroODataEntityController<TEntity, TPrimaryKey> : CodeZeroODataController
        where TPrimaryKey : IEquatable<TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected IRepository<TEntity, TPrimaryKey> Repository { get; private set; }
        
        protected CodeZeroODataEntityController(IRepository<TEntity, TPrimaryKey> repository)
        {
            Repository = repository;
        }

        [EnableQuery]
        public virtual IQueryable<TEntity> Get()
        {
            return Repository.GetAll();
        }

        [EnableQuery]
        public virtual SingleResult<TEntity> Get([FromODataUri] TPrimaryKey key)
        {
            var entity = Repository.GetAll().Where(e => e.Id.Equals(key));

            return SingleResult.Create(entity);
        }

        public virtual async Task<IActionResult> Post(TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEntity = await Repository.InsertAsync(entity);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            
            return Created(createdEntity);
        }

        public virtual async Task<IActionResult> Patch([FromODataUri] TPrimaryKey key, Delta<TEntity> entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var dbLookup = await Repository.GetAsync(key);
            if (dbLookup == null)
            {
                return NotFound();
            }
            
            entity.Patch(dbLookup);

            return Updated(entity);
        }

        public virtual async Task<IActionResult> Put([FromODataUri] TPrimaryKey key, TEntity update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!key.Equals(update.Id))
            {
                return BadRequest();
            }
            
            var updated = await Repository.UpdateAsync(update);

            return Updated(updated);
        }

        public virtual async Task<IActionResult> Delete([FromODataUri] TPrimaryKey key)
        {
            var product = await Repository.GetAsync(key);
            if (product == null)
            {
                return NotFound();
            }
            
            await Repository.DeleteAsync(key);

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
