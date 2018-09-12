using System.Collections.Generic;

namespace Checkout.Sdk.Payments
{
    public class CaptureRequest
    {
        public int? Amount { get; set; }
        public string Reference { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
    }
}