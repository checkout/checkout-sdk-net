using System.Collections.Generic;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines a request to void an existing payment.
    /// </summary>
    public class VoidRequest
    {
        /// <summary>
        /// Gets or sets the action reference you can later use to identify this void request.
        /// </summary>
        public string Reference { get; set; }
        
        /// <summary>
        /// Gets or sets metadata for the void request.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }
    }
}