namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    AlipayHkSource
{
    /// <summary>
    /// alipay_hk source Class
    /// The source of the payment
    /// </summary>
    public class AlipayHkSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the AlipayHkSource class.
        /// </summary>
        public AlipayHkSource() : base(SourceType.AlipayHk)
        {
        }
    }
}