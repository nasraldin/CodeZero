using CodeZero.Domain;
using System.Threading.Tasks;

namespace CodeZero.Mediator
{
    public interface IHandle<in T> where T : BaseDomainEvent
    {
        Task Handle(T domainEvent);
    }
}