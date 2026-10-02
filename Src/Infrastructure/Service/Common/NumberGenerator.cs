using SwaOlova.Infrastructure.Service.Orders;
using SwaOlova.Infrastructure.Service.Common.Interfaces;

namespace SwaOlova.Infrastructure.Service.Common;

public sealed class NumberGenerator : INumberGenerator, IOrderNumberGenerator
{
    public string GenerateCustomerNumber(int sequence) => $"CUST-{sequence:D6}";

    public string GenerateMerchantCode(int sequence) => $"MER-{sequence:D6}";

    public string GenerateOrderNumber(int sequence, DateTime? referenceDate = null)
    {
        var year = (referenceDate ?? DateTime.UtcNow).Year;
        return $"SO-{year}-{sequence:D6}";
    }

    public string GenerateRiderNumber(int sequence) => $"RID-{sequence:D6}";
}