using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Identity;

public class UserActivityLog : AggregateRoot<Guid>
{
    public Guid UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string EntityId { get; set; } = string.Empty;

    public DateTime ActivityDate { get; set; } = DateTime.UtcNow;

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public string? Details { get; set; }

    public UserActivityLog()
    {
        Id = Guid.NewGuid();
    }

    public UserActivityLog(Guid userId, string action, string entityName, string entityId, string? ipAddress = null, string? userAgent = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Action = action;
        EntityName = entityName;
        EntityId = entityId;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
}
