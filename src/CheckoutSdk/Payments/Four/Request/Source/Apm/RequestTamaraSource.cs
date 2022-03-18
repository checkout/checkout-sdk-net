using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source.Apm
{
    public class RequestTamaraSource : AbstractRequestSource
    {
        public RequestTamaraSource() : base(PaymentSourceType.Tamara)
        {
        }

        public Address BillingAddress { get; set; }
    }
}