using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.
    TokenDestination
{
    /// <summary>
    /// token destination Class
    /// The destination of the unreferenced refund.
    /// </summary>
    public class TokenDestination : AbstractDestination
    {
        /// <summary>
        /// Initializes a new instance of the TokenDestination class.
        /// </summary>
        public TokenDestination() : base(DestinationType.Token)
        {
        }

        /// <summary>
        /// The Checkout.com card token ID. This will be an ID with the prefix tok_.
        /// [Required]
        /// ^(tok)_(\w{26})$
        /// 30 characters
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The unreferenced refund destination account holder.
        /// [Optional]
        /// </summary>
        public AbstractAccountHolder AccountHolder { get; set; }
    }
}