namespace Checkout.Common
{
    public sealed class MarketplaceDataSubEntity
    {
        public string Id { get; set; }

        public long? Amount { get; set; }

        public class Comission
        {
            public long? Amount { get; set; }

            public double Percentage { get; set; }
        }
    }
}