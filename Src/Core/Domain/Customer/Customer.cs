using SwaOlova.Domain.Common;

namespace SwaOlova.Domain.Customer
{
    public class Customer : AggregateRoot<Guid>
    {
        public string CustomerNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? AlternativePhoneNumber { get; set; }

        public string? EmailAddress { get; set; }

        public bool IsVerified { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<CustomerAddress> Addresses { get; set; }
            = new List<CustomerAddress>();

        public ICollection<CustomerReferral> Referrals { get; set; }
            = new List<CustomerReferral>();
    }
}
