using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SwaOlova.Infrastructure.Data.Interceptors;

public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplySoftDelete(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplySoftDelete(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ApplySoftDelete(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted))
        {
            var entityType = entry.Entity.GetType();
            var isDeletedProperty = entityType.GetProperty("IsDeleted", BindingFlags.Public | BindingFlags.Instance);

            if (isDeletedProperty is null || isDeletedProperty.PropertyType != typeof(bool) || !isDeletedProperty.CanWrite)
            {
                continue;
            }

            isDeletedProperty.SetValue(entry.Entity, true);
            entry.State = EntityState.Modified;
        }
    }
}
