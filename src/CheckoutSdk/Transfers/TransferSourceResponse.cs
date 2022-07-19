using Checkout.Common;

namespace Checkout.Transfers
{
    public class TransferSourceResponse
    {
        public string EntityId { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }
    }
}