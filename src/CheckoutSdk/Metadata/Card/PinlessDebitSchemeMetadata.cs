namespace Checkout.Metadata.Card
{
    public class PinlessDebitSchemeMetadata
    {
        public string NetworkId { get; set; }

        public string NetworkDescription { get; set; }

        public bool BillPayIndicator { get; set; }

        public bool EcommerceIndicator { get; set; }

        public string InterchangeFeeIndicator { get; set; }

        public bool MoneyTransferIndicator { get; set; }

        public bool TokenIndicator { get; set; }
    }
}