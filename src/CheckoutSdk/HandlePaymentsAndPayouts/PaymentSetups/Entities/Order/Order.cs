using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Contexts;

namespace Checkout.Payments.Setups.Entities
{
    public class Order
    {
        /// <summary>
        /// The items included in the order
        /// </summary>
        public IList<PaymentContextsItems> Items { get; set; }

        /// <summary>
        /// The shipping information for the order
        /// </summary>
        public ShippingDetails Shipping { get; set; }

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
}



