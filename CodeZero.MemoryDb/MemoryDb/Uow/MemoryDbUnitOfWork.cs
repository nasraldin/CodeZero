//  <copyright file="MemoryDbUnitOfWork.cs" project="CodeZero.MemoryDb" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Dependency;
using CodeZero.Domain.Uow;
using CodeZero.MemoryDb.Configuration;

namespace CodeZero.MemoryDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MemoryDb.
    /// </summary>
    public class MemoryDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to Memory Database.
        /// </summary>
        public MemoryDatabase Database { get; private set; }

        private readonly ICodeZeroMemoryDbModuleConfiguration _configuration;
        private readonly MemoryDatabase _memoryDatabase;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MemoryDbUnitOfWork(
            ICodeZeroMemoryDbModuleConfiguration configuration, 
            MemoryDatabase memoryDatabase, 
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkFilterExecuter filterExecuter,
            IUnitOfWorkDefaultOptions defaultOptions)
            : base(
                  connectionStringResolver, 
                  defaultOptions,
                  filterExecuter)
        {
            _configuration = configuration;
            _memoryDatabase = memoryDatabase;
        }

        protected override void BeginUow()
        {
            Database = _memoryDatabase;
        }

        public override void SaveChanges()
        {

        }

        #pragma warning disable 1998
        public override async Task SaveChangesAsync()
        {

        }
        #pragma warning restore 1998

        protected override void CompleteUow()
        {

        }

        #pragma warning disable 1998
        protected override async Task CompleteUowAsync()
        {

        }
        #pragma warning restore 1998

        protected override void DisposeUow()
        {

        }
    }
}