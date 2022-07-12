using CodeZero.Domain.Entities;
using CodeZero.Domain.Entities.Auditing;

namespace WebApp.Domain.Entities;

public class Language : FullAuditedEntity<int>, IActive
{
    public string Name { get; set; } = default!;
    public string Culture { get; set; } = default!;
    public string Icon { get; set; } = default!;
    public bool IsRtl { get; set; }
    public bool IsActive { get; set; }

    public Language() { }

    public Language(
        string name,
        string culture,
        string icon,
        bool isActive = true,
        bool isRtl = false)
    {
        Name = name;
        Culture = culture;
        Icon = icon;
        IsActive = isActive;
        IsRtl = isRtl;
    }
}
