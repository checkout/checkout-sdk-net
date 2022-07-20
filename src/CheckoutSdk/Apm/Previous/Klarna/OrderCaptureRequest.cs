using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Apm.Previous.Klarna
{
    public class OrderCaptureRequest
    {
        public const PaymentSourceType Type = PaymentSourceType.Klarna;

        public long? Amount { get; set; }

        public string Reference { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public Klarna Klarna { get; set; }

        public KlarnaShippingInfo ShippingInfo { get; set; }

        public long? ShippingDelay { get; set; }
    }
}