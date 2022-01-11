namespace Checkout.Common
{
    public class MarketplaceDataSubEntity
    {
        public string Id { get; set; }

        public long? Amount { get; set; }

        public string Reference { get; set; }

        public MarketPlaceCommission Commission { get; set; }

        public class MarketPlaceCommission
        {
            public long? Amount { get; set; }

            public double Percentage { get; set; }
        }
    }
}