using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class Order
    {
        public IList<OrderItem> Items { get; set; }

        public OrderShipping Shipping { get; set; }

        public IList<OrderSubMerchant> SubMerchants { get; set; }

        public long? DiscountAmount { get; set; }
    }

    public class OrderShipping
    {
        public Address Address { get; set; }
        
        public string Method { get; set; }
    }
}



