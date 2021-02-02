using System;
using System.Collections.Generic;
using System.Threading;

namespace CodeZero.Shared.Lock
{
    /// <summary>
    /// Synchronizer for value types
    /// </summary>
    /// <typeparam name="T">Type of value</typeparam>
    public class SyncLock<T> where T : struct
    {
        #region Lock Helper Classes

        internal class SyncHandle : IDisposable
        {
            private SyncLock<T> Sync { get; set; }

            private SyncObject LockObject { get; set; }

            public SyncHandle(SyncLock<T> sync, SyncObject syncObject)
            {
                this.Sync = sync;
                this.LockObject = syncObject;
                Monitor.Enter(this.LockObject);
            }

            public override string ToString()
            {
                return string.Format("SyncHandle[Object: {0}]", this.LockObject);
            }

            public void Dispose()
            {
                this.Sync.UnLock(this.LockObject);
                Monitor.Exit(this.LockObject);
            }
        }

        internal class SyncObject
        {
            public T Value { get; private set; }

            public int Count { get; set; }

            public SyncObject(T value)
            {
                this.Value = value;
            }

            public override string ToString()
            {
                return string.Format("SyncObject[Value: {0}, Count: {1}]", this.Value, this.Count);
            }
        }

        #endregion

        private readonly Dictionary<T, SyncObject> syncObjects = new Dictionary<T, SyncObject>();

        /// <summary>
        /// Acquires an exclusive lock on the specified value, must be called in 'using' statement
        /// </summary>
        /// <example>
        /// Using(Sync.Lock(value))
        /// {
        ///     ....
        /// }
        /// </example>
        /// <param name="value">The value on which to acquire the lock.</param>
        /// <returns>Handle for lock object for T value</returns>
        public IDisposable Lock(T value)
        {
            SyncObject syncObject;

            lock (this.syncObjects)
            {
                if (!this.syncObjects.TryGetValue(value, out syncObject))
                {
                    syncObject = new SyncObject(value);
                    this.syncObjects.Add(value, syncObject);
                }

                syncObject.Count += 1;
            }

            return new SyncHandle(this, syncObject);
        }

        private void UnLock(SyncObject syncLock)
        {
            lock (this.syncObjects)
            {
                syncLock.Count -= 1;
                if (syncLock.Count == 0)
                    this.syncObjects.Remove(syncLock.Value);
            }
        }

        public override string ToString()
        {
            return string.Format("SyncLock[Locks: {0}]", this.syncObjects.Count);
        }
    }
}
