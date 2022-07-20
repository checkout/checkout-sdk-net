using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestTamaraSource : AbstractRequestSource
    {
        public RequestTamaraSource() : base(PaymentSourceType.Tamara)
        {
        }

        public Address BillingAddress { get; set; }
    }
}