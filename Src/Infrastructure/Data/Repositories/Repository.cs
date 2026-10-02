using Microsoft.EntityFrameworkCore;
using SwaOlova.Domain.Common;
using SwaOlova.Infrastructure.Data.Context;

namespace SwaOlova.Infrastructure.Data.Repositories;

public class Repository<TEntity> : SwaOlova.Application.Common.Interfaces.Repositories.IRepository<TEntity>
    where TEntity : Entity<Guid>
{
    protected readonly SwaOlavaDbContext DbContext;
    protected readonly DbSet<TEntity> Set;

    public Repository(SwaOlavaDbContext dbContext)
    {
        DbContext = dbContext;
        Set = dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Set.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Set.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Set.AddAsync(entity, cancellationToken);
    }

    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Set.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Set.Remove(entity);
        return Task.CompletedTask;
    }
}
