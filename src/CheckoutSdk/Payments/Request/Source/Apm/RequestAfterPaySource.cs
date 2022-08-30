using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestAfterPaySource : AbstractRequestSource
    {
        public AccountHolder AccountHolder { get; set; }

        public RequestAfterPaySource() : base(PaymentSourceType.Afterpay)
        {
        }
    }
}