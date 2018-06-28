//  <copyright file="SimpleLogAuditingStore.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using Castle.Core.Logging;

namespace CodeZero.Auditing
{
    /// <summary>
    /// Implements <see cref="IAuditingStore"/> to simply write audits to logs.
    /// </summary>
    public class SimpleLogAuditingStore : IAuditingStore
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static SimpleLogAuditingStore Instance { get; } = new SimpleLogAuditingStore();

        public ILogger Logger { get; set; }

        public SimpleLogAuditingStore()
        {
            Logger = NullLogger.Instance;
        }

        public Task SaveAsync(AuditInfo auditInfo)
        {
            if (auditInfo.Exception == null)
            {
                Logger.Info(auditInfo.ToString());
            }
            else
            {
                Logger.Warn(auditInfo.ToString());
            }

            return Task.FromResult(0);
        }
    }
}