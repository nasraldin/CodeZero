using System.Collections.Generic;

namespace CodeZero.Domain.Entities
{
    //TODO: Re-consider this interface
    public interface IGeneratesDomainEvents
    {
        IEnumerable<object> GetLocalEvents();

        IEnumerable<object> GetDistributedEvents();

        void ClearLocalEvents();

        void ClearDistributedEvents();
    }
}