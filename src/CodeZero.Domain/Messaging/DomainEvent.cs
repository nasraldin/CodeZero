namespace CodeZero.Domain.Messaging;

public abstract class DomainEvent : Event
{
    public bool IsPublished { get; set; }

    protected DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}