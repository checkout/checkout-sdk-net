namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.PaypalSource
{
    /// <summary>
    /// paypal source Class
    /// The source of the payment
    /// </summary>
    public class PaypalSource : AbstractSource
    {

        /// <summary>
        /// Initializes a new instance of the PaypalSource class.
        /// </summary>
        public PaypalSource() : base(SourceType.Paypal)
        {
        }

    }
}
