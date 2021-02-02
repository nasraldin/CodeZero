using System.Threading.Tasks;

namespace CodeZero.Domain
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(BaseDomainEvent domainEvent);
    }
}