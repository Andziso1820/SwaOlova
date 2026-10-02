using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SwaOlova.Domain.Common;

namespace SwaOlova.Infrastructure.Data.Interceptors;

public sealed class AuditInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAuditValues(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAuditValues(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ApplyAuditValues(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var now = DateTime.UtcNow;
        var userName = Environment.UserName;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is not AuditableEntity<Guid> entity)
            {
                continue;
            }

            if (entry.State == EntityState.Added)
            {
                entity.CreatedDate = now;
                entity.CreatedBy = string.IsNullOrWhiteSpace(entity.CreatedBy) ? userName : entity.CreatedBy;
            }

            if (entry.State == EntityState.Modified)
            {
                entity.ModifiedDate = now;
                entity.ModifiedBy = userName;
            }
        }
    }
}
