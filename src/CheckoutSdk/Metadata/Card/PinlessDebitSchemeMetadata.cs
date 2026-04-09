namespace Checkout.Metadata.Card
{
    public class PinlessDebitSchemeMetadata
    {
        /// <summary>
        /// The PINless debit network identifier.
        /// </summary>
        public string NetworkId { get; set; }

        /// <summary>
        /// The PINless debit network name.
        /// </summary>
        public string NetworkDescription { get; set; }

        /// <summary>
        /// Describes whether the card is eligible for bill payment transactions.
        /// </summary>
        public bool BillPayIndicator { get; set; }

        /// <summary>
        /// Describes whether the card is eligible for e-commerce transactions.
        /// </summary>
        public bool EcommerceIndicator { get; set; }

        /// <summary>
        /// The type of interchange fee used for transactions.
        /// </summary>
        public string InterchangeFeeIndicator { get; set; }

        /// <summary>
        /// Describes whether the card is eligible for money transfer transactions.
        /// </summary>
        public bool MoneyTransferIndicator { get; set; }

        /// <summary>
        /// True indicates that the card PAN is a DPAN, false indicates that it is a FPAN.
        /// </summary>
        public bool TokenIndicator { get; set; }
    }
}
