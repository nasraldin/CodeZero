using System;

namespace CodeZero.Messaging
{
    public abstract class DomainEvent : Event
    {
        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}