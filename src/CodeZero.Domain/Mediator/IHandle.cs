using CodeZero.Domain.Messaging;

namespace CodeZero.Domain.Mediator;

public interface IHandle<in T> where T : Event
{
    Task Handle(T domainEvent);
}