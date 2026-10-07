using Microsoft.EntityFrameworkCore;
using SwaOlova.Domain.Customer;
using SwaOlova.Infrastructure.Data.Context;
using SwaOlova.Infrastructure.Data.Repositories;

namespace SwaOlova.Infrastructure.Data.Repositories.Customers;

public sealed class CustomerRepository : Repository<Customer>, SwaOlova.Application.Common.Interfaces.Repositories.ICustomerRepository
{
    public CustomerRepository(SwaOlavaDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        return await DbContext.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Customer>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? searchTerm,
        CancellationToken cancellationToken = default)
    {
        var query = BuildSearchQuery(searchTerm);

        return await query
            .OrderBy(x => x.CustomerNumber)
            .ThenBy(x => x.FirstName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(string? searchTerm, CancellationToken cancellationToken = default)
    {
        return await BuildSearchQuery(searchTerm).CountAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<CustomerAddress>> GetAddressesAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await DbContext.CustomerAddresses
            .AsNoTracking()
            .Where(x => x.CustomerId == customerId)
            .OrderByDescending(x => x.IsDefault)
            .ThenBy(x => x.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<CustomerAddress?> GetAddressByIdAsync(Guid customerId, Guid addressId, CancellationToken cancellationToken = default)
    {
        return await DbContext.CustomerAddresses
            .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Id == addressId, cancellationToken);
    }

    public async Task AddAddressAsync(CustomerAddress address, CancellationToken cancellationToken = default)
    {
        await DbContext.CustomerAddresses.AddAsync(address, cancellationToken);
    }

    public Task UpdateAddressAsync(CustomerAddress address, CancellationToken cancellationToken = default)
    {
        DbContext.CustomerAddresses.Update(address);
        return Task.CompletedTask;
    }

    public Task DeleteAddressAsync(CustomerAddress address, CancellationToken cancellationToken = default)
    {
        DbContext.CustomerAddresses.Remove(address);
        return Task.CompletedTask;
    }

    private IQueryable<Customer> BuildSearchQuery(string? searchTerm)
    {
        var query = DbContext.Customers.AsNoTracking();

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return query;
        }

        var term = searchTerm.Trim();
        return query.Where(x =>
            EF.Functions.Like(x.CustomerNumber, $"%{term}%") ||
            EF.Functions.Like(x.FirstName, $"%{term}%") ||
            EF.Functions.Like(x.LastName, $"%{term}%") ||
            EF.Functions.Like(x.PhoneNumber, $"%{term}%") ||
            (x.EmailAddress != null && EF.Functions.Like(x.EmailAddress, $"%{term}%")));
    }
}
