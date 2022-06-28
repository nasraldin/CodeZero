//using CodeZero.Domain.Mediator;
//using CodeZero.Domain.Messaging;

//namespace CodeZero.Domain;

//// https://gist.github.com/jbogard/54d6569e883f63afebc7
//// http://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
//public class DomainEventDispatcher : IDomainEventDispatcher
//{
//    private readonly IContainer _container;

//    public DomainEventDispatcher(IContainer container)
//    {
//        _container = container;
//    }

//    public Task Dispatch(Event domainEvent)
//    {
//        var handlerType = typeof(IHandle<>).MakeGenericType(domainEvent.GetType());
//        var wrapperType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());
//        var handlers = _container.GetAllInstances(handlerType);
//        var wrappedHandlers = handlers
//            .Cast<object>()
//            .Select(handler => (DomainEventHandler)Activator.CreateInstance(wrapperType, handler));

//        foreach (var handler in wrappedHandlers)
//        {
//            handler.Handle(domainEvent);
//        }

//        return Task.CompletedTask;
//    }

//    private abstract class DomainEventHandler
//    {
//        public abstract void Handle(Event domainEvent);
//    }

//    private sealed class DomainEventHandler<T> : DomainEventHandler where T : Event
//    {
//        private readonly IHandle<T> _handler;

//        public DomainEventHandler(IHandle<T> handler)
//        {
//            _handler = handler;
//        }

//        public override void Handle(Event domainEvent)
//        {
//            _handler.Handle((T)domainEvent);
//        }
//    }
//}