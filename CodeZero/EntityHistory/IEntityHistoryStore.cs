//  <copyright file="IEntityHistoryStore.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;

namespace CodeZero.EntityHistory
{
    /// <summary>
    /// This interface should be implemented by vendors to
    /// make entity history working.
    /// </summary>
    public interface IEntityHistoryStore
    {
        /// <summary>
        /// Should save entity change set to a persistent store.
        /// </summary>
        /// <param name="entityChangeSet">Entity change set</param>
        Task SaveAsync(EntityChangeSet entityChangeSet);
    }
}
