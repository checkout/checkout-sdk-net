using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestWeChatPaySource : AbstractRequestSource
    {
        public Address BillingAddress { get; set; }

        public RequestWeChatPaySource() : base(PaymentSourceType.Wechatpay)
        {
        }
    }
}