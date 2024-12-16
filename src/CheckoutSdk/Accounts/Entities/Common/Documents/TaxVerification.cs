namespace Checkout.Accounts.Entities.Common.Documents
{
    public class TaxVerification
    {
        public TaxVerificationType? Type { get; set; }
        
        public string Front { get; set; }
    }
}