using Checkout.Common;
using System;

namespace Checkout.Issuing.Cardholders
{
    public class CardholderDetailsResponse : Resource
    {
        public string Id { get; set; }

        public CardholderType? Type { get; set; }

        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Phone PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }

        public Address BillingAddress { get; set; }

        public Address ResidencyAddress { get; set; }

        public string Reference { get; set; }

        public string AccountEntityId { get; set; }

        public string ParentSubEntityId { get; set; }

        public string EntityId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}