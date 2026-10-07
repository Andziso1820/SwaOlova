using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Product
{
    public class Inventory : AuditableEntity<Guid>
    {
        public Guid ProductId { get; set; }

        public int QuantityAvailable { get; set; }

        public bool InStock { get; set; }
    }
}
