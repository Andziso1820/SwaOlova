using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Identity;

public class RolePermission : AggregateRoot<Guid>
{
    public string RoleName { get; set; } = string.Empty;

    public Guid PermissionId { get; set; }

    public Permission? Permission { get; set; }

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

    public RolePermission()
    {
        Id = Guid.NewGuid();
    }

    public RolePermission(string roleName, Guid permissionId)
    {
        Id = Guid.NewGuid();
        RoleName = roleName;
        PermissionId = permissionId;
    }
}
