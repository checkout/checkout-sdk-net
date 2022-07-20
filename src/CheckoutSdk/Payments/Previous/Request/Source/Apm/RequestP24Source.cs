using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestP24Source : AbstractRequestSource
    {
        public CountryCode? PaymentCountry { get; set; }

        public string AccountHolderName { get; set; }

        public string AccountHolderEmail { get; set; }

        public string BillingDescriptor { get; set; }

        public RequestP24Source() : base(PaymentSourceType.Przelewy24)
        {
        }
    }
}