namespace Checkout.Marketplace
{
    public class MarketplaceFilePurpose
    {
        private MarketplaceFilePurpose(string purpose)
        {
            Value = purpose;
        }

        public string Value { get; private set; }

        public static MarketplaceFilePurpose BankVerification
        {
            get
            {
                return new MarketplaceFilePurpose("bank_verification");
            }
        }

        public static MarketplaceFilePurpose Identification
        {
            get
            {
                return new MarketplaceFilePurpose("identification");
            }
        }
    }
}