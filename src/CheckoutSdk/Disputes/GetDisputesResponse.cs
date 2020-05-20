using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Disputes
{
    /// <summary>
    /// Is the <see cref="GetDisputesResponse"/> containing all disputes.
    /// </summary>
    public class GetDisputesResponse : Resource
    {
        /// <summary>
        /// Gets or sets the requested number of items to return.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets the requested number of results to skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the requested ISO-8601 date and time from which to filter disputes, based on the dispute's last_update field.
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// Gets or sets the requested ISO-8601 date and time until which to filter disputes, based on the dispute's last_update field.
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// Gets or sets one or more requested comma-separated statuses.
        /// </summary>
        public string Statuses { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the dispute.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the requested unique identifier of the payment.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the requested optional reference (such as an order ID) that you can use later to identify the payment.
        /// </summary>
        public string PaymentReference { get; set; }

        /// <summary>
        /// Gets or sets the requested acquirer reference number (ARN).
        /// </summary>
        public string PaymentArn { get; set; }

        /// <summary>
        /// Gets or sets the requested boolean for showing disputes on channel or business level.
        /// </summary>
        public bool ThisChannelOnly { get; set; }

        /// <summary>
        /// Gets or sets the total number of disputes retrieved (without taking into consideration skip and limit parameters).
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the list of disputes found via the request.
        /// </summary>
        public List<DisputeSummary> Data { get; set; }
    }
}
