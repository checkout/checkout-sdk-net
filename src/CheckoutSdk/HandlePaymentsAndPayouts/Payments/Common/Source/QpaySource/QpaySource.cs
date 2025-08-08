namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    QpaySource
{
    /// <summary>
    /// qpay source Class
    /// The source of the payment
    /// </summary>
    public class QpaySource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the QpaySource class.
        /// </summary>
        public QpaySource() : base(SourceType.Qpay)
        {
        }

        /// <summary>
        /// Alphanumeric string containing a description of the payment order.
        /// [Required]
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// QPay Payment Unique Number
        /// [Required]
        /// </summary>
        public string Pun { get; set; }

        /// <summary>
        /// The status code returned from the QPay gateway on payment, if available.
        /// [Optional]
        /// </summary>
        public string QpayStatus { get; set; }

        /// <summary>
        /// A message giving further detail on the payment status, for failure/cancelled/success status payments.
        /// [Optional]
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// An identifier from the QPay gateway for a successful payment.
        /// [Optional]
        /// </summary>
        public string ConfirmationId { get; set; }
    }
}