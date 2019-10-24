using System.Collections.Generic;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines a request to refund an existing payment.
    /// </summary>
    public class RefundRequest
    {
        /// <summary>
        /// Gets or sets the amount to refund in the major currency. 
        /// If not specified, the full payment amount will be refunded.
        /// </summary>
        public long? Amount { get; set; }
        
        
        /// <summary>
        /// Gets or sets the action reference you can later use to identify this refund request.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets metadata for the refund request.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }
    }
}