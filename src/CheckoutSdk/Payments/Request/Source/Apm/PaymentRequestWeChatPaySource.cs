using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class PaymentRequestWeChatPaySource : AbstractRequestSource
    {
        public Address BillingAddress { get; set; }

        public PaymentRequestWeChatPaySource() : base(PaymentSourceType.Wechatpay)
        {
        }
    }
}