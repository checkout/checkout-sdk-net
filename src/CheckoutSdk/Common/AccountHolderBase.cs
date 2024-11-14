namespace Checkout.Common
{
    public class AccountHolderBase
    {
        public AccountHolderType? Type { get; set; }

        public string FullName { get; set; }
        
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Gender { get; set; }

        public string CompanyName { get; set; }

        public string TaxId { get; set; }
        
        public string DateOfBirth { get; set; }

        public CountryCode? CountryOfBirth { get; set; }

        public string ResidentialStatus { get; set; } 
        
        public Address BillingAddress { get; set; }
        
        public Phone Phone { get; set; }

        public AccountHolderIdentification Identification { get; set; }

    }
}