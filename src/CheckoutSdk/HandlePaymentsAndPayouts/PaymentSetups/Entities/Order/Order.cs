using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Contexts;

namespace Checkout.Payments.Setups.Entities
{
    public class Order
    {
        /// <summary>
        /// A list of items in the order
        /// [Optional]
        /// </summary>
        public IList<PaymentContextsItems> Items { get; set; }

        /// <summary>
        /// The customer's shipping details
        /// [Optional]
        /// </summary>
        public ShippingDetails Shipping { get; set; }

        /// <summary>
        /// The sub-merchants' details
        /// [Optional]
        /// </summary>
        public IList<OrderSubMerchant> SubMerchants { get; set; }

        /// <summary>
        /// The unique identifier for the invoice.
        /// [Optional]
        /// </summary>
        public string InvoiceId { get; set; }

        /// <summary>
        /// The total shipping amount for the order.
        /// [Optional]
        /// &gt;= 0
        /// </summary>
        public long? ShippingAmount { get; set; }

        /// <summary>
        /// The discount amount the merchant applied to the transaction
        /// [Optional]
        /// &gt;= 0
        /// </summary>
        public long? DiscountAmount { get; set; }

        /// <summary>
        /// The total tax amount for the order.
        /// [Optional]
        /// &gt;= 0
        /// </summary>
        public long? TaxAmount { get; set; }
    }
}



