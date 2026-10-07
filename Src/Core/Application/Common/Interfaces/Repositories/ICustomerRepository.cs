using SwaOlova.Domain.Customer;

namespace SwaOlova.Application.Common.Interfaces.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Customer>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? searchTerm,
        CancellationToken cancellationToken = default);
    Task<int> CountAsync(string? searchTerm, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CustomerAddress>> GetAddressesAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<CustomerAddress?> GetAddressByIdAsync(Guid customerId, Guid addressId, CancellationToken cancellationToken = default);
    Task AddAddressAsync(CustomerAddress address, CancellationToken cancellationToken = default);
    Task UpdateAddressAsync(CustomerAddress address, CancellationToken cancellationToken = default);
    Task DeleteAddressAsync(CustomerAddress address, CancellationToken cancellationToken = default);
}