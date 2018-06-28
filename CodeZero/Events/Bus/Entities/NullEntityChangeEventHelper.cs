//  <copyright file="NullEntityChangeEventHelper.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;

namespace CodeZero.Events.Bus.Entities
{
    /// <summary>
    /// Null-object implementation of <see cref="IEntityChangeEventHelper"/>.
    /// </summary>
    public class NullEntityChangeEventHelper : IEntityChangeEventHelper
    {
        /// <summary>
        /// Gets single instance of <see cref="NullEntityChangeEventHelper"/> class.
        /// </summary>
        public static NullEntityChangeEventHelper Instance { get; } = new NullEntityChangeEventHelper();

        private NullEntityChangeEventHelper()
        {

        }

        public void TriggerEntityCreatingEvent(object entity)
        {
            
        }

        public void TriggerEntityCreatedEventOnUowCompleted(object entity)
        {
            
        }

        public void TriggerEntityUpdatingEvent(object entity)
        {
            
        }

        public void TriggerEntityUpdatedEventOnUowCompleted(object entity)
        {
            
        }

        public void TriggerEntityDeletingEvent(object entity)
        {
            
        }

        public void TriggerEntityDeletedEventOnUowCompleted(object entity)
        {
            
        }

        public void TriggerEvents(EntityChangeReport changeReport)
        {
            
        }

        public Task TriggerEventsAsync(EntityChangeReport changeReport)
        {
            return Task.FromResult(0);
        }
    }
}