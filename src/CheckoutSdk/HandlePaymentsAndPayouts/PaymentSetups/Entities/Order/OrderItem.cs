using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class OrderItem
    {
        public string Name { get; set; }

        public long? Quantity { get; set; }

        public long? UnitPrice { get; set; }
        
        public long? TotalAmount { get; set; }
        
        public string Reference { get; set; }        


        public long? DiscountAmount { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string Type { get; set; }
    }
}