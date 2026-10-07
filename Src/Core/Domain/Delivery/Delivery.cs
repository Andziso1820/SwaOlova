using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Delivery
{
    public class Delivery : AggregateRoot<Guid>
    {
        public Guid OrderId { get; set; }

        public Guid RiderId { get; set; }

        public DeliveryStatus Status { get; set; }

        public DateTime? PickupTime { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public decimal DistanceKm { get; set; }

        public decimal EstimatedDurationMinutes { get; set; }
    }
}
