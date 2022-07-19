using Checkout.Common;

namespace Checkout.Transfers
{
    public class CreateTransferResponse : Resource
    {
        public string Id { get; set; }

        public TransferStatus? Status { get; set; }
    }
}