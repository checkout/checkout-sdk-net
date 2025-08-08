namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source.
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