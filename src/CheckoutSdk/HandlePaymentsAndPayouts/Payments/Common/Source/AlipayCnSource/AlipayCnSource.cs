namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    AlipayCnSource
{
    /// <summary>
    /// alipay_cn source Class
    /// The source of the payment
    /// </summary>
    public class AlipayCnSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the AlipayCnSource class.
        /// </summary>
        public AlipayCnSource() : base(SourceType.AlipayCn)
        {
        }
    }
}