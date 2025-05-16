using Checkout.Common;

namespace Checkout.Issuing.Cardholders.Requests
{
    public class CardholderRequest
    {
        public CardholderType? Type { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public Address BillingAddress { get; set; }
        
        public string EntityId { get; set; }
      
        public string Reference { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public Phone PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }

        public Address ResidencyAddress { get; set; }

        public CardholderDocument Document { get; set; }
        
    }
}