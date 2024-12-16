using Checkout.Common;

namespace Checkout.Accounts.Entities.Common.Company
{
    public class Individual
    {
        // Common
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public DateOfBirth DateOfBirth { get; set; }

        public PlaceOfBirth PlaceOfBirth { get; set; }
        
        public Address Address { get; set; }

        public string MiddleName { get; set; }
        
        public string NationalIdNumber { get; set; }
        
        public string EmailAddress { get; set; }
        
        public Phone Phone { get; set; }
        
        public FinancialDetails FinancialDetails { get; set; }
        
        public Identification Identification { get; set; }
        
        public Address RegisteredAddress { get; set; }
        public string TradingName { get; set; }
        
        // Unknown
        
        public string LegalName { get; set; }
        public string NationalTaxId { get; set; }
        
    }
}
