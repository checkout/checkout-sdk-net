using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Marketplace
{
    public class MarketplaceAccountHolder
    {
        public AccountHolderType? Type { get; set; }

        public string TaxId { get; set; }

        public DateOfBirth DateOfBirth { get; set; }

        public CountryCode? CountryOfBirth { get; set; }

        public string ResidentialStatus { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }

        public Identification Identification { get; set; }

        public string Email { get; set; }
    }
}