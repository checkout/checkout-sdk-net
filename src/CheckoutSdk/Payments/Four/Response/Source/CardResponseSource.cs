using Checkout.Common;

namespace Checkout.Payments.Four.Response.Source
{
    public class CardResponseSource : AbstractResponseSource, IResponseSource
    {
        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Scheme { get; set; }

        public string Last4 { get; set; }

        public string Fingerprint { get; set; }

        public string Bin { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public string AvsCheck { get; set; }

        public string CvvCheck { get; set; }

        public bool? Payouts { get; set; }

        public string FastFunds { get; set; }

        public string PaymentAccountReference { get; set; }

        public new PaymentSourceType? Type()
        {
            return base.Type;
        }
       
    }
}