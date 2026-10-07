using SwaOlova.Domain.Customer;

namespace SwaOlova.Infrastructure.Service.Customers;

public interface ICustomerService
{
    string GenerateCustomerNumber(int sequence);

    bool Validate(Customer customer);

    string BuildSummary(Customer customer);

    Task<Customer?> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);

    Task<(int TotalCustomers, int ActiveCustomers)> GetStatisticsAsync(CancellationToken cancellationToken = default);
}