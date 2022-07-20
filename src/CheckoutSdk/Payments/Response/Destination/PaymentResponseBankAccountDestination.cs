using Checkout.Common;

namespace Checkout.Payments.Response.Destination
{
    public class PaymentResponseBankAccountDestination : AbstractPaymentResponseDestination,
        IPaymentResponseDestination
    {
        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }

        public string Name { get; set; }

        public string Last4 { get; set; }

        public string Fingerprint { get; set; }

        public string Bin { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public string Issuer { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public new PaymentDestinationType? Type()
        {
            return base.Type;
        }
    }
}