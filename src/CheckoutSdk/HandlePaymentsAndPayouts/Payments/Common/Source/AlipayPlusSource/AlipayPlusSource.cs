namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    AlipayPlusSource
{
    /// <summary>
    /// alipay_plus source Class
    /// The source of the payment
    /// </summary>
    public class AlipayPlusSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the AlipayPlusSource class.
        /// </summary>
        public AlipayPlusSource() : base(SourceType.AlipayPlus)
        {
        }
    }
}