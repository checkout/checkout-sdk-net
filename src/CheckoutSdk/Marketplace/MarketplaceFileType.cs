namespace Checkout.Marketplace
{
    public class MarketplaceFileType
    {
        public string Value { get; }

        private MarketplaceFileType(string type)
        {
            Value = type;
        }

        public static MarketplaceFileType Passport
            => new MarketplaceFileType("passport");

        public static MarketplaceFileType NationalIdentityCard
            => new MarketplaceFileType("national_identity_card");

        public static MarketplaceFileType DrivingLicence
            => new MarketplaceFileType("driving_licence");

        public static MarketplaceFileType CitizenCard
            => new MarketplaceFileType("citizen_card");

        public static MarketplaceFileType ResidencePermit
            => new MarketplaceFileType("residence_permit");

        public static MarketplaceFileType ElectoralId
            => new MarketplaceFileType("electoral_id");

        public static MarketplaceFileType BankStatement
            => new MarketplaceFileType("bank_statement");
    }
}