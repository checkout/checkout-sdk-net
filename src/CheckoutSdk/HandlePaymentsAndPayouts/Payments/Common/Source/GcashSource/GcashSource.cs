namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    GcashSource
{
    /// <summary>
    /// gcash source Class
    /// The source of the payment
    /// </summary>
    public class GcashSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the GcashSource class.
        /// </summary>
        public GcashSource() : base(SourceType.Gcash)
        {
        }
    }
}