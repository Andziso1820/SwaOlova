using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Promotions
{
    public class Promotion : AggregateRoot<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public decimal DiscountValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
