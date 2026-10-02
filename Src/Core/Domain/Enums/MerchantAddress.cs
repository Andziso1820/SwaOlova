using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Enums
{
    public class MerchantAddress : AuditableEntity<Guid>
    {
        public string AddressLine1 { get; set; } = string.Empty;

        public string Village { get; set; } = string.Empty;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
