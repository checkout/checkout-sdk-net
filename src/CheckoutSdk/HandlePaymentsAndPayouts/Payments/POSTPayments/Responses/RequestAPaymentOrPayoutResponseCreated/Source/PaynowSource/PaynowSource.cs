namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source.
    PaynowSource
{
    /// <summary>
    /// paynow source Class
    /// The source of the payment
    /// </summary>
    public class PaynowSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the PaynowSource class.
        /// </summary>
        public PaynowSource() : base(SourceType.Paynow)
        {
        }
    }
}