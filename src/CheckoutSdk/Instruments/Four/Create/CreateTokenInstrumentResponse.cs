using Checkout.Common;

namespace Checkout.Instruments.Four.Create
{
    public sealed class CreateTokenInstrumentResponse : CreateInstrumentResponse
    {
        public CreateTokenInstrumentResponse() : base(InstrumentType.Card)
        {
        }

        public string Fingerprint { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Scheme { get; set; }

        public string Last4 { get; set; }

        public string Bin { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public CustomerResponse Customer { get; set; }
    }
}