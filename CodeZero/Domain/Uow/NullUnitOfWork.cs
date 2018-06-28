//  <copyright file="NullUnitOfWork.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;

namespace CodeZero.Domain.Uow
{
    /// <summary>
    /// Null implementation of unit of work.
    /// It's used if no component registered for <see cref="IUnitOfWork"/>.
    /// This ensures working CodeZero without a database.
    /// </summary>
    public sealed class NullUnitOfWork : UnitOfWorkBase
    {
        public override void SaveChanges()
        {

        }

        public override Task SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        protected override void BeginUow()
        {

        }

        protected override void CompleteUow()
        {

        }

        protected override Task CompleteUowAsync()
        {
            return Task.FromResult(0);
        }

        protected override void DisposeUow()
        {

        }

        public NullUnitOfWork(
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkDefaultOptions defaultOptions,
            IUnitOfWorkFilterExecuter filterExecuter
            ) : base(
                connectionStringResolver,
                defaultOptions,
                filterExecuter)
        {
        }
    }
}
