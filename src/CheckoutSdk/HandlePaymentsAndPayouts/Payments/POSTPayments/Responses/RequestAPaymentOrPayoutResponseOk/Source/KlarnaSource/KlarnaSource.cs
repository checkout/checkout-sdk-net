namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseOk.Source.
    KlarnaSource
{
    /// <summary>
    /// klarna source Class
    /// The source of the payment
    /// </summary>
    public class KlarnaSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the KlarnaSource class.
        /// </summary>
        public KlarnaSource() : base(SourceType.Klarna)
        {
        }
    }
}