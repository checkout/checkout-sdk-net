using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Marketplace
{
    public class MarketplaceAccountHolder : AccountHolder
    {
        public MarketplaceAccountHolderType Type { get; set; }

        public string CompanyName { get; set; }

        public string TaxId { get; set; }

        public DateOfBirth DateOfBirth { get; set; }

        public CountryCode? CountryOfBirth { get; set; }

        public string ResidentialStatus { get; set; }

        public Identification Identification { get; set; }

        public string Email { get; set; }
    }
}