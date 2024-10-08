using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestSequraSource : AbstractRequestSource
    {
        public RequestSequraSource() : base(PaymentSourceType.Sequra)
        {
        }

        public Address BillingAddress { get; set; }
    }
}