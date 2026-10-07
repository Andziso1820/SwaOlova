using SwaOlova.Infrastructure.Data.Context;

namespace SwaOlova.Infrastructure.Data.UnitOfWork;

public sealed class UnitOfWork : Application.Common.Interfaces.Repositories.IUnitOfWork
{
    private readonly SwaOlavaDbContext _dbContext;

    public UnitOfWork(SwaOlavaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
