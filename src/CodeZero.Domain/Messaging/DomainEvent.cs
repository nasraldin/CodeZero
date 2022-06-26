namespace CodeZero.Domain.Messaging;

public abstract class DomainEvent : Event
{
    protected DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}