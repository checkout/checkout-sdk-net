using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsOrder
    {
        public IList<PaymentSetupsOrderItem> Items { get; set; }

        public PaymentSetupsOrderShipping Shipping { get; set; }

        public IList<PaymentSetupsOrderSubMerchant> SubMerchants { get; set; }

        public long? DiscountAmount { get; set; }
    }
}