using SwaOlova.Domain.Auditing;

namespace SwaOlova.Application.Common.Interfaces.Repositories;

public interface IAuditRepository : IRepository<AuditLog>
{
}