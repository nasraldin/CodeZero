using System.Collections.ObjectModel;

namespace CodeZero.Domain.Aggregate;

[Serializable]
public abstract class BasicAggregateRoot : IAggregateRoot, IGeneratesDomainEvents
{
    private readonly ICollection<object> _distributedEvents = new Collection<object>();
    private readonly ICollection<object> _localEvents = new Collection<object>();

    public virtual IEnumerable<object> GetLocalEvents()
    {
        return _localEvents;
    }

    public virtual IEnumerable<object> GetDistributedEvents()
    {
        return _distributedEvents;
    }

    public virtual void ClearLocalEvents()
    {
        _localEvents.Clear();
    }

    public virtual void ClearDistributedEvents()
    {
        _distributedEvents.Clear();
    }

    protected virtual void AddLocalEvent(object eventData)
    {
        _localEvents.Add(eventData);
    }

    protected virtual void AddDistributedEvent(object eventData)
    {
        _distributedEvents.Add(eventData);
    }
}