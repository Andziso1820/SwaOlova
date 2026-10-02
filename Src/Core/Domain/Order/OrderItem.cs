using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Order
{
    public class OrderItem : AuditableEntity<Guid>
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
