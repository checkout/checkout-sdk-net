using Checkout.Common;

namespace Checkout.Marketplace.Transfer
{
    public class CreateTransferResponse : Resource
    {
        public string Id { get; set; }

        public string Status { get; set; }
    }
}