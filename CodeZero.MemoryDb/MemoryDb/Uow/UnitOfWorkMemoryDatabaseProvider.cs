//  <copyright file="UnitOfWorkMemoryDatabaseProvider.cs" project="CodeZero.MemoryDb" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;
using CodeZero.Domain.Uow;

namespace CodeZero.MemoryDb.Uow
{
    /// <summary>
    /// Implements <see cref="IMemoryDatabaseProvider"/> that gets database from active unit of work.
    /// </summary>
    public class UnitOfWorkMemoryDatabaseProvider : IMemoryDatabaseProvider, ITransientDependency
    {
        public MemoryDatabase Database { get { return ((MemoryDbUnitOfWork)_currentUnitOfWork.Current).Database; } }

        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWork;

        public UnitOfWorkMemoryDatabaseProvider(ICurrentUnitOfWorkProvider currentUnitOfWork)
        {
            _currentUnitOfWork = currentUnitOfWork;
        }
    }
}