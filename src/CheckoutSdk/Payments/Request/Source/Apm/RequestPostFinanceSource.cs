using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestPostFinanceSource : AbstractRequestSource
    {
        public CountryCode? PaymentCountry { get; set; }

        public string AccountHolderName { get; set; }

        public string BillingDescriptor { get; set; }

        public RequestPostFinanceSource() : base(PaymentSourceType.Postfinance)
        {
        }
    }
}