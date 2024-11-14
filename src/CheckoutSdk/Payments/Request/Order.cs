using System;

namespace Checkout.Payments.Request
{
    public class Order
    {
        public string Name { get; set; }

        public long? Quantity { get; set; }

        public long? UnitPrice { get; set; }

        public string Reference { get; set; }
        
        public string CommodityCode { get; set; }
        
        public string UnitOfMeasure { get; set; }
        
        public long? TotalAmount { get; set; }

        public long? TaxAmount { get; set; }
        
        public long? TaxRate { get; set; }

        public long? DiscountAmount { get; set; }
        
        public string WxpayGoodsId { get; set; }
        
        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }
        
        public DateTime ServiceEndsOn { get; set; }
    }
}