using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Customer
{
    public class CustomerAddress : AuditableEntity<Guid>
    {
        public Guid CustomerId { get; set; }

        public string AddressLine1 { get; set; } = string.Empty;

        public string Village { get; set; } = string.Empty;

        public string Landmark { get; set; } = string.Empty;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? GatePhotoUrl { get; set; }

        public bool IsDefault { get; set; }
    }
}
