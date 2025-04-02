using Checkout.Common;

namespace Checkout.Issuing.Transactions.Responses
{
    public class TotalReversed
    {
        public long? Amount { get; set; }
        
        public Currency? Currency { get; set; }
    }
}