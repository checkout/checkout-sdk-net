using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.AgenticCommerce.Responses
{
    /// <summary>
    /// The response returned when a delegated payment token is successfully created.
    /// </summary>
    public class DelegatedPaymentResponse : Resource
    {
        /// <summary>
        /// The unique identifier of the provisioned payment token.
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The date and time the token was created, in RFC 3339 format.
        /// [Required]
        /// Format: date-time.
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// A set of key-value pairs containing response metadata.
        /// [Required]
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; }
    }
}
