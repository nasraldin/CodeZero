using CodeZero.Domain.Messaging;

namespace CodeZero.Domain;

public interface IDomainEventDispatcher
{
    Task Dispatch(Event domainEvent);
}