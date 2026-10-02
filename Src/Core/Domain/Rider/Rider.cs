using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Rider
{
    public class Rider : AggregateRoot<Guid>
    {
        public string RiderNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string DriversLicenseNumber { get; set; } = string.Empty;

        public RiderStatus Status { get; set; }
    }
}
