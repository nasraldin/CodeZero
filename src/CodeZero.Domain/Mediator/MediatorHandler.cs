using System.Runtime.CompilerServices;
using CodeZero.Domain.Messaging;
using FluentValidation.Results;
using MediatR;

namespace CodeZero.Domain.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual async Task PublishEvent<T>(T @event) where T : Event
    {
        await _mediator.Publish(@event);
    }
}