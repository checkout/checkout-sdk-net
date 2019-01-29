using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments
{
    /// <summary>
    /// Summary of a payment action.
    /// </summary>
    public class PaymentActionSummary
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
        /// Gets the Checkout.com Gateway response code.
        /// </summary>
        public string ResponseCode { get; set; }
        
        /// <summary>
        /// Gets the response summary.
        /// </summary>
        public string ResponseSummary { get; set; }
    }
}