using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.
    CardDestination
{
    /// <summary>
    /// card destination Class
    /// The destination of the unreferenced refund.
    /// </summary>
    public class CardDestination : AbstractDestination
    {
        /// <summary>
        /// Initializes a new instance of the CardDestination class.
        /// </summary>
        public CardDestination() : base(DestinationType.Card)
        {
        }

        /// <summary>
        /// The card number.
        /// [Required]
        /// &lt;= 19
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The card's expiration month.
        /// [Required]
        /// [ 1 .. 2 ] characters [ 1 .. 12 ]
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The card's expiration year.
        /// [Required]
        /// 4 characters
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// The unreferenced refund destination account holder.
        /// [Required]
        /// </summary>
        public AbstractAccountHolder AccountHolder { get; set; }
    }
}