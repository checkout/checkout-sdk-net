namespace Checkout.Common
{
    public sealed class Product
    {
        public string Name { get; set; }

        public long? Quantity { get; set; }

        public long? Price { get; set; }
    }
}