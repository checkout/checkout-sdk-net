using Checkout.Common;

namespace Checkout.Issuing.Transactions.Responses
{
    public class TotalHeld
    {
        public long? Amount { get; set; }
        
        public Currency? Currency { get; set; }
    }
}