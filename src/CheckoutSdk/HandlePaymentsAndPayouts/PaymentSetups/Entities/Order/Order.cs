using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Contexts;

namespace Checkout.Payments.Setups.Entities
{
    public class Order
    {
        /// <summary>
        /// A list of items in the order
        /// </summary>
        public IList<PaymentContextsItems> Items { get; set; }

        /// <summary>
        /// The customer's shipping details
        /// </summary>
        public ShippingDetails Shipping { get; set; }

        /// <summary>
        /// The sub-merchants' details
        /// </summary>
        public IList<OrderSubMerchant> SubMerchants { get; set; }

        /// <summary>
        /// The discount amount the merchant applied to the transaction
        /// &gt;= 0
        /// </summary>
        public long? DiscountAmount { get; set; }
    }
}



