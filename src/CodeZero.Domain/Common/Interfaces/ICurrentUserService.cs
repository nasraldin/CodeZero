namespace CodeZero.Domain.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
}