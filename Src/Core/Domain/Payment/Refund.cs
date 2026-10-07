using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Payment
{
    public class Refund : AuditableEntity<Guid>
    {
        public Guid PaymentId { get; set; }

        public decimal Amount { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
