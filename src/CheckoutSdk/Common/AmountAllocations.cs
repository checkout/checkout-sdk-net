namespace Checkout.Common
{
    public class AmountAllocations
    {
        public string Id { get; set; }

        public long? Amount { get; set; }

        public string Reference { get; set; }

        public Commission Commission { get; set; }
    }
}