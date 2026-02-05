namespace Checkout.StandaloneAccountUpdater.Entities
{
    public class CardUpdated : CardBase
    {
        /// <summary>
        /// The encrypted full Primary Account Number (PAN). Returned only for PCI SAQ D merchants.
        /// </summary>
        public string EncryptedCardNumber { get; set; }

        /// <summary>
        /// The first 6 digits of the PAN.
        /// </summary>
        public string Bin { get; set; }

        /// <summary>
        /// Last 4 digits of the PAN.
        /// </summary>
        public string Last4 { get; set; }

        /// <summary>
        /// Unique identifier for the card
        /// </summary>
        public string Fingerprint { get; set; }
    }
}