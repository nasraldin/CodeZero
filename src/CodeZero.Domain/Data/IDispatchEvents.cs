namespace CodeZero.Domain;

public interface IDispatchEvents
{
    Task<bool> DispatchEvents();
}