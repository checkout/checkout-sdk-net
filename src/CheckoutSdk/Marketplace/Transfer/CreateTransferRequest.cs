namespace Checkout.Marketplace.Transfer
{
    public class CreateTransferRequest
    {
        public string Reference { get; set; }

        public TransferType? TransferType { get; set; }

        public TransferSource Source { get; set; }

        public TransferDestination Destination { get; set; }
    }
}