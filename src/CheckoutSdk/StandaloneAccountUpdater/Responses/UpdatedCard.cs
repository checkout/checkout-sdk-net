namespace Checkout.StandaloneAccountUpdater.Responses
{
    public class UpdatedCard
    {
        /// <summary>
        /// Expiry month. Expressed in number.
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Four-digit expiry year.
        /// </summary>
        public int ExpiryYear { get; set; }

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