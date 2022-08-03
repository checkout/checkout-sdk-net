namespace Checkout.Marketplace
{
    public class EntityFinancialDetails
    {
        public long? AnnualProcessingVolume { get; set; }

        public long? AverageTransactionValue { get; set; }

        public long? HighestTransactionValue { get; set; }

        public EntityFinancialDocuments Documents { get; set; }
    }
}