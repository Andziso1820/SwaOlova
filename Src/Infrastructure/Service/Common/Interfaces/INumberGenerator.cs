namespace SwaOlova.Infrastructure.Service.Common.Interfaces;

public interface INumberGenerator
{
    string GenerateCustomerNumber(int sequence);

    string GenerateMerchantCode(int sequence);

    string GenerateOrderNumber(int sequence, DateTime? referenceDate = null);

    string GenerateRiderNumber(int sequence);
}