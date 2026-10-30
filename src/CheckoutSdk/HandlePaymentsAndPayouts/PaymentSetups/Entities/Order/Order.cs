using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Contexts;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The customer's order details.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// A list of items in the order.
        /// [Optional]
        /// </summary>
        public IList<PaymentContextsItems> Items { get; set; }

        /// <summary>
        /// The customer's shipping details.
        /// [Optional]
        /// </summary>
        public ShippingDetails Shipping { get; set; }

        /// <summary>
        /// The details of the sub-merchants.
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
        /// min 0
        /// </summary>
        public long? ShippingAmount { get; set; }

        /// <summary>
        /// The discount amount the merchant applied to the transaction.
        /// [Optional]
        /// min 0
        /// </summary>
        public long? DiscountAmount { get; set; }

        /// <summary>
        /// The total tax amount for the order.
        /// [Optional]
        /// min 0
        /// </summary>
        public long? TaxAmount { get; set; }

        /// <summary>
        /// The total tipping amount for the order.
        /// [Optional]
        /// Format: int64
        /// min 0
        /// </summary>
        public long? TippingAmount { get; set; }

        /// <summary>
        /// The total surcharge amount for the order.
        /// [Optional]
        /// Format: int64
        /// min 0
        /// </summary>
        public long? SurchargeAmount { get; set; }

        /// <summary>
        /// The sub-entities that the payment is being processed on behalf of.
        /// [Optional]
        /// min 1 item, max 50 items
        /// </summary>
        public IList<PaymentSetupAmountAllocation> AmountAllocations { get; set; }
    }
}
