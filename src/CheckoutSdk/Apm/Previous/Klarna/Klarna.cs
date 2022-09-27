using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Apm.Previous.Klarna
{
    public class Klarna
    {
        public string Description { get; set; }

        public IList<KlarnaProduct> Products { get; set; }

        public IList<ShippingInfo> ShippingInfo { get; set; }

        public long? ShippingDelay { get; set; }
    }
}