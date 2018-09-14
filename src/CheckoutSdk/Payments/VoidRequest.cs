using System.Collections.Generic;

namespace Checkout.Payments
{
    public class VoidRequest
    {
        public string Reference { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
    }
}