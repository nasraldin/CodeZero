using CodeZero.Domain.Entities;
using CodeZero.Domain.Entities.Auditing;

namespace WebApp.Domain;

/// <summary>
/// Base entity.
/// </summary>
[Serializable]
public abstract class BaseEntity : FullAuditedEntity<int>, IActive
{
    public bool IsActive { get; protected set; }
}
