namespace Checkout.Accounts.Entities.Common.Documents
{
    public class IdentityVerification
    {
        public IdentityVerificationType? Type { get; set; }
        
        public string Front { get; set; }
        
        public string Back { get; set; }
    }
}