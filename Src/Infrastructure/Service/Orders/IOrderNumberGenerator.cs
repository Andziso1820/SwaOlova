namespace SwaOlova.Infrastructure.Service.Orders;

public interface IOrderNumberGenerator
{
    string GenerateOrderNumber(int sequence, DateTime? referenceDate = null);
}