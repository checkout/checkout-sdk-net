using Checkout.Common;

namespace Checkout.Marketplace.Transfer
{
    public class CreateTransferResponse : Resource
    {
        public string Id { get; set; }

        public TransferStatus? Status { get; set; }
    }
}