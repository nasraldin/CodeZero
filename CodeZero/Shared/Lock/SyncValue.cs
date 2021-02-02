using System.Collections.Generic;

namespace CodeZero.Shared.Lock
{
    /// <summary>
    /// Unique object for value of T
    /// </summary>
    /// <typeparam name="T">Type of value</typeparam>
    public class SyncValue<T> where T : struct
    {
        private readonly Dictionary<T, object> syncObjects = new Dictionary<T, object>();

        /// <summary>
        /// Unique object for value of T
        /// </summary>
        /// <param name="value">The value to acquire unique object</param>
        /// <returns>Unique object for value</returns>
        public object GetObject(T value)
        {
            lock (syncObjects)
            {
                if (!syncObjects.TryGetValue(value, out object syncObject))
                    syncObjects.Add(value, syncObject = new object());

                return syncObject;
            }
        }
    }
}
