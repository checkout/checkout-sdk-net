using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Disputes
{
    /// <summary>
    /// Represents a <see cref="DisputeSummary"/> as returned in <see cref="GetDisputesResponse.Data"/>.
    /// </summary>
    public class DisputeSummary : Resource
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
        /// Gets or sets the identifier of the payment to which the dispute relates.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the reference of the payment to which the dispute relates.
        /// </summary>
        public string PaymentReference { get; set; }

        /// <summary>
        /// Gets or sets the method of the payment to which the dispute relates.
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the acquirer reference number (ARN) of the payment to which the dispute relates.
        /// </summary>
        public string PaymentArn { get; set; }

        /// <summary>
        /// Gets or sets the ISO-8601 date and time at which the dispute was issued.
        /// </summary>
        public DateTime ReceivedOn { get; set; }

        /// <summary>
        /// Gets or sets the ISO-8601 date and time at which the dispute was last updated.
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Gets or sets the deadline by which to respond to the dispute.
        /// This corresponds to received_on + n, where n is a number of calendar days set by the scheme / acquirer.
        /// </summary>
        public DateTime EvidenceRequiredBy { get; set; }
    }
}
