using Checkout.Common;

namespace Checkout.Payments.Four.Request.Source.Apm
{
    public class PaymentRequestWeChatPaySource : AbstractRequestSource
    {
        public Address BillingAddress { get; set; }

        public PaymentRequestWeChatPaySource() : base(PaymentSourceType.Wechatpay)
        {
        }
    }
}