using System.Collections.Generic;

namespace Checkout.Payments.Four
{
    public class RefundRequest
    {
        public long? Amount { get; set; }

        public string Reference { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}