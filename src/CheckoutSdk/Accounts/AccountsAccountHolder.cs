using Checkout.Common;

namespace Checkout.Accounts
{
    public class AccountsAccountHolder
    {
        public AccountHolderType? Type { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string TaxId { get; set; }

        public DateOfBirth DateOfBirth { get; set; }

        public CountryCode? CountryOfBirth { get; set; }

        public string ResidentialStatus { get; set; }
        
        public Address BillingAddress { get; set; }
        
        public AccountPhone Phone { get; set; }

        public AccountHolderIdentification Identification { get; set; }

        public string Email { get; set; }
    }
}