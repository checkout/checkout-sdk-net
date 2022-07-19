namespace Checkout.Transfers
{
    public class CreateTransferRequest
    {
        public string Reference { get; set; }

        public TransferType? TransferType { get; set; }

        public TransferSourceRequest Source { get; set; }

        public TransferDestinationRequest Destination { get; set; }
    }
}