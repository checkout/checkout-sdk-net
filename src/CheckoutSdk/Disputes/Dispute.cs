using Checkout.Common;

namespace Checkout.Disputes
{
    public class Dispute : Resource
    {
        /// <summary>
        /// Gets or sets the unique identifier of the dispute.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the reason for the dispute. <see href="https://docs.checkout.com/docs/disputes#section-dispute-reasons-and-recommended-evidence">Find out more</see>.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the current status of the dispute.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the amount that is being disputed, in the processing currency.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency the payment was made in.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the unique payment identifier.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the optional reference (such as an order ID) that you can use later to identify the payment.
        /// </summary>
        public string PaymentReference { get; set; }

        /// <summary>
        /// Gets or sets the acquirer reference number that can be used to query the issuing bank.
        /// </summary>
        public string PaymentArn { get; set; }

        /// <summary>
        /// Gets or sets the payment method / card scheme.
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the deadline by which to respond to the dispute.
        /// This corresponds to received_on + n, where n is a number of calendar days set by the scheme / acquirer.
        /// </summary>
        public string EvidenceRequiredBy { get; set; }

        /// <summary>
        /// Gets or sets the ISO-8601 date and time at which the dispute was issued.
        /// </summary>
        public string ReceivedOn { get; set; }

        /// <summary>
        /// Gets or sets the ISO-8601 date and time at which the dispute was last updated.
        /// </summary>
        public string LastUpdate { get; set; }
    }
}
