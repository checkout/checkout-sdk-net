using System.Collections.Generic;

namespace Checkout.Payments
{
    public class VoidRequest
    {
        /// <summary>
        /// The amount to void, min 0, max 9999999999. If not specified, the full payment amount is voided.
        /// </summary>
        public long? Amount { get; set; }

        public string Reference { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}