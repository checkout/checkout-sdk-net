using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestAlmaSource : AbstractRequestSource
    {
        public RequestAlmaSource() : base(PaymentSourceType.Alma)
        {
        }

        public Address BillingAddress { get; set; }
    }
}