using Checkout.Common;

namespace Checkout.Sessions
{
    public class SessionsCardMetadataResponse
    {
        public CardType CardType { get; set; }

        public CardCategory CardCategory { get; set; }

        public string IssuerName { get; set; }

        public CountryCode IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }
    }
}