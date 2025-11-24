using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class Order
    {
        /// <summary>
        /// The items included in the order
        /// </summary>
        public IList<OrderItem> Items { get; set; }

        /// <summary>
        /// The shipping information for the order
        /// </summary>
        public OrderShipping Shipping { get; set; }

        /// <summary>
        /// Information about sub-merchants involved in the order
        /// </summary>
        public IList<OrderSubMerchant> SubMerchants { get; set; }

        /// <summary>
        /// &gt;= 0
        /// The discount amount applied to the order
        /// </summary>
        public long? DiscountAmount { get; set; }
    }

    public class OrderShipping
    {
        /// <summary>
        /// The shipping address for the order
        /// </summary>
        public Address Address { get; set; }
        
        /// <summary>
        /// The shipping method to be used
        /// </summary>
        public string Method { get; set; }
    }
}



