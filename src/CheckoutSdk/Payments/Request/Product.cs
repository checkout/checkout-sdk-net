namespace Checkout.Payments.Request
{
    public class Product
    {
        public string Name { get; set; }

        public long? Quantity { get; set; }

        public long? UnitPrice { get; set; }

        public string Reference { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public long? TotalAmount { get; set; }

        public long? TaxAmount { get; set; }

        public long? DiscountAmount { get; set; }

        public string Sku { get; set; }

        public string GoodsId { get; set; }

        public string WxpayGoodsId { get; set; }
    }
}