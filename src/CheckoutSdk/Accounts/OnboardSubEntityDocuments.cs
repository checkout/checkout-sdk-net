namespace Checkout.Accounts
{
    public class OnboardSubEntityDocuments
    {
        public Document IdentityVerification { get; set; }

        public CompanyVerificationType? CompanyVerification { get; set; }

        public TaxVerificationType? TaxVerification { get; set; }
    }
}