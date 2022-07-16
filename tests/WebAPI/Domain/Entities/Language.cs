namespace WebAPI.Domain.Entities;

public class Language : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Culture { get; set; } = default!;
    public string Icon { get; set; } = default!;
    public bool IsRtl { get; set; }

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