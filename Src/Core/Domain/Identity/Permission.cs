using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Identity;

public class Permission : AggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Module { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public Permission()
    {
        Id = Guid.NewGuid();
    }

    public Permission(string name, string code, string module, string? description = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
        Module = module;
        Description = description;
    }
}
