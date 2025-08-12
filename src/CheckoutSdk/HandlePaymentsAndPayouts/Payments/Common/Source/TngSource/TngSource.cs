namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    TngSource
{
    /// <summary>
    /// tng source Class
    /// The source of the payment
    /// </summary>
    public class TngSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the TngSource class.
        /// </summary>
        public TngSource() : base(SourceType.Tng)
        {
        }
    }
}