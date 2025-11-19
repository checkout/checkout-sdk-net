using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsOrder
    {
        public IList<PaymentSetupsOrderItem> Items { get; set; }

        public PaymentSetupsOrderShipping Shipping { get; set; }

        public IList<PaymentSetupsOrderSubMerchant> SubMerchants { get; set; }

        public long? DiscountAmount { get; set; }
    }

    public class PaymentSetupsOrderItem
    {
        public string Name { get; set; }

        public int? Quantity { get; set; }

        public long? UnitPrice { get; set; }

        public long? TotalAmount { get; set; }

        public string Reference { get; set; }

        public long? DiscountAmount { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string Type { get; set; }
    }

    public class PaymentSetupsOrderShipping
    {
        public Address Address { get; set; }

        public string Method { get; set; }
    }

    public class PaymentSetupsOrderSubMerchant
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}