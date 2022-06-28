namespace CodeZero.Domain;

//TODO: Re-consider this interface
public interface IGeneratesDomainEvents
{
    IEnumerable<object> GetLocalEvents();
    IEnumerable<object> GetDistributedEvents();
    void ClearLocalEvents();
    void ClearDistributedEvents();
}