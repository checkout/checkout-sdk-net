using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsOrderItem : Product
    {
        public long? UnitPrice { get; set; }

        public long? TotalAmount { get; set; }

        public long? DiscountAmount { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string Type { get; set; }
    }
}