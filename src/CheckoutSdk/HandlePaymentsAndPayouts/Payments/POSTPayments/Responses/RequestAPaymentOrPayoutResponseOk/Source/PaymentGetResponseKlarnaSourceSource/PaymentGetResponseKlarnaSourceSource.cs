using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseOk.Source;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.PaymentGetResponseKlarnaSourceSource
{
    /// <summary>
    /// PaymentGetResponseKlarnaSource source Class
    /// The source of the payment
    /// </summary>
    public class PaymentGetResponseKlarnaSourceSource : AbstractSource
    {

        /// <summary>
        /// Initializes a new instance of the PaymentGetResponseKlarnaSourceSource class.
        /// </summary>
        public PaymentGetResponseKlarnaSourceSource() : base(GetResponseKlarnaSourceSourceType.Payment)
        {
        }

        /// <summary>
        /// object describes payee details
        /// [Optional]
        /// </summary>
        public AccountHolder AccountHolder { get; set; }

    }
}
