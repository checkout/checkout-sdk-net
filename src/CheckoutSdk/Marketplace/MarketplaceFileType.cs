namespace Checkout.Marketplace
{
    public class MarketplaceFileType
    {
        public string Value { get; private set; }

        private MarketplaceFileType(string type)
        {
            Value = type;
        }

        public static MarketplaceFileType Passport
        { get { return new MarketplaceFileType("passport"); } }

        public static MarketplaceFileType NationalIdentityCard
        { get { return new MarketplaceFileType("national_identity_card"); } }

        public static MarketplaceFileType DrivingLicence
        { get { return new MarketplaceFileType("driving_licence"); } }

        public static MarketplaceFileType CitizenCard
        { get { return new MarketplaceFileType("citizen_card"); } }

        public static MarketplaceFileType ResidencePermit
        { get { return new MarketplaceFileType("residence_permit"); } }

        public static MarketplaceFileType ElectoralId
        { get { return new MarketplaceFileType("electoral_id"); } }

        public static MarketplaceFileType BankStatement
        { get { return new MarketplaceFileType("bank_statement"); } }
    }
}