using Checkout.Common;

namespace Checkout.Transfers
{
    public class TransferSourceRequest
    {
        public string Id { get; set; }

        public long? Amount { get; set; }
        
        public Currency? Currency { get; set; }
    }
}