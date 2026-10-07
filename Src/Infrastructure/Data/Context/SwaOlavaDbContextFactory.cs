using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SwaOlova.Infrastructure.Data.Context;

public sealed class SwaOlavaDbContextFactory : IDesignTimeDbContextFactory<SwaOlavaDbContext>
{
    public SwaOlavaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SwaOlavaDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SwaOlavaDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        return new SwaOlavaDbContext(optionsBuilder.Options);
    }
}
