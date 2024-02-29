namespace Checkout.Accounts
{
    public class OnboardSubEntityDocuments
    {
        public Document IdentityVerification { get; set; }

        public CompanyVerification CompanyVerification { get; set; }

        public TaxVerification TaxVerification { get; set; }
    }
}