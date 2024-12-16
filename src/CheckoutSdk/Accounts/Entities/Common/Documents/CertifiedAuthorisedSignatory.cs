namespace Checkout.Accounts.Entities.Common.Documents
{
    public class CertifiedAuthorisedSignatory
    {
        public CertifiedAuthorisedSignatoryType? Type { get; set; }
        
        public string Front { get; set; }
        
        public string Back { get; set; }
    }
}