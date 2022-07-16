namespace CodeZero.Domain;

public interface IHasDomainEvent
{
    List<DomainEvent> DomainEvents { get; set; }
}