//  <copyright file="IUnitOfWorkDefaultOptions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Transactions;

namespace CodeZero.Domain.Uow
{
    /// <summary>
    /// Used to get/set default options for a unit of work.
    /// </summary>
    public interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Scope option.
        /// </summary>
        TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// Should unit of works be transactional.
        /// Default: true.
        /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        /// A boolean value indicates that System.Transactions.TransactionScope is available for current application.
        /// Default: true.
        /// </summary>
        bool IsTransactionScopeAvailable { get; set; }

        /// <summary>
        /// Gets/sets a timeout value for unit of works.
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets/sets isolation level of transaction.
        /// This is used if <see cref="IsTransactional"/> is true.
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Gets list of all data filter configurations.
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// A list of selectors to determine conventional Unit Of Work classes.
        /// </summary>
        List<Func<Type, bool>> ConventionalUowSelectors { get; }

        /// <summary>
        /// Registers a data filter to unit of work system.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default.</param>
        void RegisterFilter(string filterName, bool isEnabledByDefault);

        /// <summary>
        /// Overrides a data filter definition.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default.</param>
        void OverrideFilter(string filterName, bool isEnabledByDefault);
    }
}