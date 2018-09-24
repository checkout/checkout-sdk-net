using System.Collections.Generic;

namespace Checkout.Payments
{
    public class RefundRequest
    {
        /// <summary>
        /// The amount to refund in the major currency. If not specified, the full payment amount will be refunded
        /// </summary>
        public int? Amount { get; set; }
        /// <summary>
        /// A reference you can later use to identify this refund request
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// Set of key/value pairs that you can attach to the refund request. It can be useful for storing additional information in a structured format
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }
    }
}