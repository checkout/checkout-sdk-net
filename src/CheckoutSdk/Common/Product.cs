namespace Checkout.Common
{
    public class Product
    {
        public string Name { get; set; }

        public long? Quantity { get; set; }

        public long? Price { get; set; }
        
        public string Reference { get; set; }
    }
}