namespace Checkout.Marketplace
{
    public class MarketplaceFilePurpose
    {
        private MarketplaceFilePurpose(string purpose)
        {
            Value = purpose;
        }

        public string Value { get; }

        public static MarketplaceFilePurpose BankVerification => new MarketplaceFilePurpose("bank_verification");

        public static MarketplaceFilePurpose Identification => new MarketplaceFilePurpose("identification");
        
        public static MarketplaceFilePurpose CompanyVerification => new MarketplaceFilePurpose("company_verification");
        
    }
}