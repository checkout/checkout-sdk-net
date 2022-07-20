using Checkout.Common;

namespace Checkout.Instruments.Previous
{
    public class CreateInstrumentResponse : HttpMetadata
    {
        public string Id { get; set; }

        public InstrumentType? Type { get; set; }

        public string Fingerprint { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Last4 { get; set; }

        public string Bin { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public string Scheme { get; set; }

        public InstrumentAccountHolder AccountHolder { get; set; }

        public InstrumentCustomerResponse Customer { get; set; }
    }
}