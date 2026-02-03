using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.
    NetworkTokenDestination
{
    /// <summary>
    /// network_token destination Class
    /// The destination of the unreferenced refund.
    /// </summary>
    public class NetworkTokenDestination : AbstractDestination
    {
        /// <summary>
        /// Initializes a new instance of the NetworkTokenDestination class.
        /// </summary>
        public NetworkTokenDestination() : base(DestinationType.NetworkToken)
        {
        }

        /// <summary>
        /// The network token's Primary Account Number (PAN).
        /// [Required]
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The network token's expiration month.
        /// [Required]
        /// [ 1 .. 2 ] characters  >= 1
        /// &gt;= 1
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The network token's expiration year.
        /// [Required]
        /// 4 characters
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// The network token type.
        /// [Required]
        /// </summary>
        public TokenType TokenType { get; set; }

        /// <summary>
        /// The network token's Base64-encoded cryptographic identifier (TAVV).
        /// The cryptogram is used by card schemes to validate the token verification result.
        /// [Required]
        /// &lt;= 50
        /// </summary>
        public string Cryptogram { get; set; }

        /// <summary>
        /// The network token's Electronic Commerce Indicator (ECI) security level.
        /// [Required]
        /// &lt;= 2
        /// </summary>
        public string Eci { get; set; }

        /// <summary>
        /// The unreferenced refund destination account holder.
        /// [Required]
        /// </summary>
        public AbstractAccountHolder AccountHolder { get; set; }
    }
}