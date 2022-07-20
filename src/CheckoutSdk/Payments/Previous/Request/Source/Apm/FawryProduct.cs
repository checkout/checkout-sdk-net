namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class FawryProduct
    {
        public string ProductId { get; set; }

        public long? Quantity { get; set; }

        public long? Price { get; set; }

        public string Description { get; set; }
    }
}