using System.Collections.Generic;

namespace Checkout.Payments
{
    public class VoidRequest
    {
        public string Reference { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}