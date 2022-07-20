using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestBancontactSource : AbstractRequestSource
    {
        public CountryCode? PaymentCountry { get; set; }

        public string AccountHolderName { get; set; }

        public string BillingDescriptor { get; set; }

        public string Language { get; set; }

        public RequestBancontactSource() : base(PaymentSourceType.Bancontact)
        {
        }
    }
}