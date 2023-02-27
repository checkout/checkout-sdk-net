using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public class RefundRequest
    {
        public long? Amount { get; set; }
        
        // Not available on Previous
        public IList<AmountAllocations> AmountAllocations { get; set; }

        public string Reference { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}