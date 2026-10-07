using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Customer
{
    public class CustomerReferral : AuditableEntity<Guid>
    {
        public Guid ReferrerCustomerId { get; set; }

        public Guid ReferredCustomerId { get; set; }

        public decimal RewardAmount { get; set; }

        public bool RewardClaimed { get; set; }
    }
}
