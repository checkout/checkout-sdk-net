using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.
    IdDestination
{
    /// <summary>
    /// id destination Class
    /// The destination of the unreferenced refund.
    /// </summary>
    public class IdDestination : AbstractDestination
    {
        /// <summary>
        /// Initializes a new instance of the IdDestination class.
        /// </summary>
        public IdDestination() : base(DestinationType.Id)
        {
        }

        /// <summary>
        /// The payment source ID. This will be an ID with the prefix src_.
        /// [Required]
        /// ^(src)_(\w{26})$
        /// 30 characters
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The unreferenced refund destination account holder.
        /// [Optional]
        /// </summary>
        public AbstractAccountHolder AccountHolder { get; set; }
    }
}