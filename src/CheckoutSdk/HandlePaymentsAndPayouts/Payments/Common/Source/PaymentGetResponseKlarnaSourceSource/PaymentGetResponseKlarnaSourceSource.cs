namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    PaymentGetResponseKlarnaSourceSource
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
        public PaymentGetResponseKlarnaSourceSource() : base(SourceType.PaymentGetResponseKlarnaSource)
        {
        }

        /// <summary>
        /// object describes payee details
        /// [Optional]
        /// </summary>
        public AccountHolder.AccountHolder AccountHolder { get; set; }
    }
}