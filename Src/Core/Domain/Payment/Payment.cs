using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Payment
{
    public class Payment : AggregateRoot<Guid>
    {
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public PaymentStatus Status { get; set; }

        public string? ExternalReference { get; set; }
    }
}
