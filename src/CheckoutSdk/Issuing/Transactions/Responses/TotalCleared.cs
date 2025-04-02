using Checkout.Common;

namespace Checkout.Issuing.Transactions.Responses
{
    public class TotalCleared
    {
        public long? Amount { get; set; }
        
        public Currency? Currency { get; set; }
    }
}