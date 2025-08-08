namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source.
    KakaopaySource
{
    /// <summary>
    /// kakaopay source Class
    /// The source of the payment
    /// </summary>
    public class KakaopaySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the KakaopaySource class.
        /// </summary>
        public KakaopaySource() : base(SourceType.Kakaopay)
        {
        }
    }
}