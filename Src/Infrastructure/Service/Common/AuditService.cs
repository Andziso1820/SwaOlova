using Microsoft.Extensions.Logging;
using SwaOlova.Infrastructure.Service.Common.Interfaces;

namespace SwaOlova.Infrastructure.Service.Common;

public sealed class AuditService(ILogger<AuditService> logger) : IAuditService
{
    public void LogCreate(string entityName, Guid entityId, string? userId = null)
    {
        logger.LogInformation("Created {EntityName} {EntityId} by {UserId}", entityName, entityId, userId ?? "system");
    }

    public void LogUpdate(string entityName, Guid entityId, string? userId = null)
    {
        logger.LogInformation("Updated {EntityName} {EntityId} by {UserId}", entityName, entityId, userId ?? "system");
    }

    public void LogDelete(string entityName, Guid entityId, string? userId = null)
    {
        logger.LogInformation("Deleted {EntityName} {EntityId} by {UserId}", entityName, entityId, userId ?? "system");
    }
}