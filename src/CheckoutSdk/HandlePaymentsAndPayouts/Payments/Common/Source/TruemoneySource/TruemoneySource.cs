namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    TruemoneySource
{
    /// <summary>
    /// truemoney source Class
    /// The source of the payment
    /// </summary>
    public class TruemoneySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the TruemoneySource class.
        /// </summary>
        public TruemoneySource() : base(SourceType.Truemoney)
        {
        }
    }
}