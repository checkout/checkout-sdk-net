using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines a payment action.
    /// </summary>
    public class PaymentAction : Resource
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment action.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of action.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the date/time the action was processed.
        /// </summary>
        public DateTime ProcessedOn { get; set; }

        /// <summary>
        /// Gets or sets the action amount.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Gets or sets the acquirer authorization code where applicable.
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// Gets or sets the Checkout.com Gateway response code.
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the response summary.
        /// </summary>
        public string ResponseSummary { get; set; }

        /// <summary>
        /// Gets or sets your reference for the action.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the key/value pairs that were attached to the action.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// Gets or sets the approved flag for the action.
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// Gets or sets information related to the processing of a payment action
        /// </summary>
        /// <value></value>
        public ActionProcessingResponse Processing { get; set; }
    }
}