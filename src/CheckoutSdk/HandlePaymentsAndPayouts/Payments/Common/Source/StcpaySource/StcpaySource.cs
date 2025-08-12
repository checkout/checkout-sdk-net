namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    StcpaySource
{
    /// <summary>
    /// stcpay source Class
    /// The source of the payment
    /// </summary>
    public class StcpaySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the StcpaySource class.
        /// </summary>
        public StcpaySource() : base(SourceType.Stcpay)
        {
        }
    }
}