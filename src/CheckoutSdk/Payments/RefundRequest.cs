using Checkout.Common;
using Checkout.Payments.Request;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public class RefundRequest
    {
        public long? Amount { get; set; }

        public string Reference { get; set; }
        
        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
        
        // Not available on Previous
        public IList<AmountAllocations> AmountAllocations { get; set; }

        public string CaptureActionId { get; set; }
        
        public Destination Destination { get; set; }
        
        public IList<Order> Items { get; set; }

    }
}