using System.Collections.Generic;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines a request to capture an existing payment.
    /// </summary>
    public class CaptureRequest
    {
        /// <summary>
        /// Gets or sets the amount to capture in the major currency. 
        /// If not specified, the full payment amount will be captured.
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// Gets or sets the action reference you can later use to identify this capture request.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets metadata for the capture request.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }
    }
}