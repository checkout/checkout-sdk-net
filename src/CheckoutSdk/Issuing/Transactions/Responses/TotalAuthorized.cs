using Checkout.Common;

namespace Checkout.Issuing.Transactions.Responses
{
    public class TotalAuthorized
    {
        public long? Amount { get; set; }
        
        public Currency? Currency { get; set; }
    }
}