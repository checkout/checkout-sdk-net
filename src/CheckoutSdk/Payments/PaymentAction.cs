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
        /// Gets the unique identifier of the payment action.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets the type of action.
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Gets the date/time the action was processed.
        /// </summary>
        public DateTime ProcessedOn { get; set; }
        
        /// <summary>
        /// Gets the action amount.
        /// </summary>
        public int Amount { get; set; }
        
        /// <summary>
        /// Gets the acquirer authorization code where applicable.
        /// </summary>
        public string AuthCode { get; set; }
        
        /// <summary>
        /// Gets the Checkout.com Gateway response code.
        /// </summary>
        public string ResponseCode { get; set; }
        
        /// <summary>
        /// Gets the response summary.
        /// </summary>
        public string ResponseSummary { get; set; }
        
        /// <summary>
        /// Gets your reference for the action.
        /// </summary>
        public string Reference { get; set; }
        
        /// <summary>
        /// Gets the key/value pairs that were attached to the action.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }
    }
}