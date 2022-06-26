using CodeZero.Domain.Messaging;

namespace CodeZero.Domain.Events;

public interface IEventStore
{
    void Save<T>(T theEvent) where T : Event;
}