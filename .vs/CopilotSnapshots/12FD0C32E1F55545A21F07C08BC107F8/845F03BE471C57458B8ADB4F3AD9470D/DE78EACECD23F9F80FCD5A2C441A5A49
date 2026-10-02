using SwaOlova.Domain.Common;
using SwaOlova.Domain.Enums;

namespace SwaOlova.Domain.Order
{
    public class Order : AggregateRoot<Guid>
    {
        public string OrderNumber { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }

        public Guid MerchantId { get; set; }

        public Guid DeliveryAddressId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderType OrderType { get; set; }

        public OrderStatus Status { get; set; }

        public decimal SubTotal { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal Total { get; set; }

        public ICollection<OrderItem> Items { get; set; }
            = new List<OrderItem>();
    }
}
