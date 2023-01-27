using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestCvConnectSource : AbstractRequestSource
    {
        public RequestCvConnectSource() : base(PaymentSourceType.Cvconnect)
        {
        }

        public Address BillingAddress { get; set; }
    }
}