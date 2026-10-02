namespace SwaOlova.Domain.Identity;

/// <summary>
/// Represents the current logged-in user context for MVC/Razor Pages applications.
/// Used in application services and controllers to access current user information and permissions.
/// </summary>
public class CurrentUser
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public List<string> Permissions { get; set; } = new();

    public List<string> Roles { get; set; } = new();

    public string FullName { get; set; } = string.Empty;

    public string? ProfilePhotoUrl { get; set; }

    public bool IsActive { get; set; }

    public bool IsLocked { get; set; }

    public Guid? RiderId { get; set; }

    public Guid? MerchantId { get; set; }

    public Guid? CustomerId { get; set; }

    public DateTime LoginDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Check if the current user has a specific permission.
    /// </summary>
    public bool HasPermission(string permissionCode)
    {
        return Permissions.Contains(permissionCode);
    }

    /// <summary>
    /// Check if the current user has any of the specified permissions.
    /// </summary>
    public bool HasAnyPermission(params string[] permissionCodes)
    {
        return permissionCodes.Any(code => Permissions.Contains(code));
    }

    /// <summary>
    /// Check if the current user has all of the specified permissions.
    /// </summary>
    public bool HasAllPermissions(params string[] permissionCodes)
    {
        return permissionCodes.All(code => Permissions.Contains(code));
    }

    /// <summary>
    /// Check if the current user has a specific role.
    /// </summary>
    public bool HasRole(string role)
    {
        return Roles.Contains(role);
    }

    /// <summary>
    /// Check if the current user has any of the specified roles.
    /// </summary>
    public bool HasAnyRole(params string[] roleNames)
    {
        return roleNames.Any(role => Roles.Contains(role));
    }
}
