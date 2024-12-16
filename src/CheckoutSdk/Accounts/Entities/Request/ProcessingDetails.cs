using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Request
{
    public class ProcessingDetails
    {
        public string SettlementCountry { get; set; }
        
        public IList<string> TargetCountries { get; set; }
        
        public int? AnnualProcessingVolume { get; set; }
        
        public int? AverageTransactionValue { get; set; }
        
        public int? HighestTransactionValue { get; set; }
        
        public Currency? Currency { get; set; }
    }
}