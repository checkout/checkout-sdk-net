using Checkout.Common;

namespace Checkout.Accounts.Entities.Common.Company
{
    public class FinancialDetails
    {
        public long? AnnualProcessingVolume { get; set; }

        public long? AverageTransactionValue { get; set; }

        public long? HighestTransactionValue { get; set; }

        
        public Currency? Currency { get; set; }
        
        // Unknown
        public FinancialDocuments Documents { get; set; }
    }
}