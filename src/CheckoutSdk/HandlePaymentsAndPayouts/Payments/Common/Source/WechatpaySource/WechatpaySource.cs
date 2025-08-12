namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    WechatpaySource
{
    /// <summary>
    /// wechatpay source Class
    /// The source of the payment
    /// </summary>
    public class WechatpaySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the WechatpaySource class.
        /// </summary>
        public WechatpaySource() : base(SourceType.Wechatpay)
        {
        }
    }
}