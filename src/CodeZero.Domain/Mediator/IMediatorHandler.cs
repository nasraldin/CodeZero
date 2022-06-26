using CodeZero.Domain.Messaging;
using FluentValidation.Results;

namespace CodeZero.Domain.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T @event) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
}