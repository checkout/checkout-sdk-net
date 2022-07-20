using System.Collections.Generic;

namespace Checkout.Apm.Previous.Klarna
{
    public class Klarna
    {
        public string Description { get; set; }

        public IList<KlarnaProduct> Products { get; set; }

        public IList<KlarnaShippingInfo> ShippingInfo { get; set; }

        public long? ShippingDelay { get; set; }
    }
}