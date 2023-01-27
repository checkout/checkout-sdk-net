using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestTrustlySource : AbstractRequestSource
    {
        public RequestTrustlySource() : base(PaymentSourceType.Trustly)
        {
        }

        public Address BillingAddress { get; set; }
    }
}