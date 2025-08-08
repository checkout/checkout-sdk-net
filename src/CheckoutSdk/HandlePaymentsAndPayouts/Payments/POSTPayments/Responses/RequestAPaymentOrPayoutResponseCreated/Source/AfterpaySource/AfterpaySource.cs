namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source.
    AfterpaySource
{
    /// <summary>
    /// afterpay source Class
    /// The source of the payment
    /// </summary>
    public class AfterpaySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the AfterpaySource class.
        /// </summary>
        public AfterpaySource() : base(SourceType.Afterpay)
        {
        }
    }
}