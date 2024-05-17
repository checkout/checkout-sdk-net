namespace Checkout.Common
{
    public class Destination
    {
        public AccountType AccountType { get; set; }
        
        public string AccountNumber { get; set; }
        
        public string BankCode { get; set; }
        
        public string BranchCode { get; set; }
        
        public string Iban { get; set; }
        
        public string Bban { get; set; }
        
        public string SwiftBic { get; set; }
        
        public CountryCode Country { get; set; }
        
        public AccountHolder AccountHolder { get; set; }
        
        public BankDetails Bank  { get; set; }
    }
}