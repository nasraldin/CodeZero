namespace CodeZero.Domain.Data;

public interface IDispatchEvents
{
    Task<bool> DispatchEvents();
}