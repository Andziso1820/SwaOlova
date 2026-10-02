namespace SwaOlova.Infrastructure.Service.Common.Interfaces;

public interface IAuditService
{
    void LogCreate(string entityName, Guid entityId, string? userId = null);

    void LogUpdate(string entityName, Guid entityId, string? userId = null);

    void LogDelete(string entityName, Guid entityId, string? userId = null);
}