namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    AlmaSource
{
    /// <summary>
    /// alma source Class
    /// The source of the payment
    /// </summary>
    public class AlmaSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the AlmaSource class.
        /// </summary>
        public AlmaSource() : base(SourceType.Alma)
        {
        }
    }
}