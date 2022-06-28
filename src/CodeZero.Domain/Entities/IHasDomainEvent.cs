namespace CodeZero.Domain.Entities;

public interface IHasDomainEvent
{
    List<DomainEvent> DomainEvents { get; set; }
}
