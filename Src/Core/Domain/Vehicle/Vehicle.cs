using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Vehicle
{
    public class Vehicle : AggregateRoot<Guid>
    {
        public Guid RiderId { get; set; }

        public string RegistrationNumber { get; set; } = string.Empty;

        public string VehicleType { get; set; } = string.Empty;

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;
    }
}
