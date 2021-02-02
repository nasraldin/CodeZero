using System;
using System.Data;

namespace CodeZero.Shared.Extensions.Transactions
{
    public static class IsolationLevelExtensions
    {
        /// <summary>
        /// Converts <see cref="System.Transactions.IsolationLevel"/> to <see cref="IsolationLevel"/>.
        /// </summary>
        public static IsolationLevel ToSystemDataIsolationLevel(this System.Transactions.IsolationLevel isolationLevel)
        {
            return isolationLevel switch
            {
                System.Transactions.IsolationLevel.Chaos => IsolationLevel.Chaos,
                System.Transactions.IsolationLevel.ReadCommitted => IsolationLevel.ReadCommitted,
                System.Transactions.IsolationLevel.ReadUncommitted => IsolationLevel.ReadUncommitted,
                System.Transactions.IsolationLevel.RepeatableRead => IsolationLevel.RepeatableRead,
                System.Transactions.IsolationLevel.Serializable => IsolationLevel.Serializable,
                System.Transactions.IsolationLevel.Snapshot => IsolationLevel.Snapshot,
                System.Transactions.IsolationLevel.Unspecified => IsolationLevel.Unspecified,
                _ => throw new Exception("Unknown isolation level: " + isolationLevel),
            };
        }
    }
}