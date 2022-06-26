using MediatR;

namespace CodeZero.Domain.Messaging;

/// <summary>
/// BaseDomainEvent
/// </summary>
public abstract class Event : Message, INotification
{
    public DateTime Timestamp { get; private set; }

    protected Event()
    {
        Timestamp = DateTime.UtcNow;
    }
}