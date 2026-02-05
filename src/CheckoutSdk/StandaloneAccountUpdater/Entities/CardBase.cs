namespace Checkout.StandaloneAccountUpdater.Entities
{
    /// <summary>
    /// Base class for card expiry date information
    /// </summary>
    public abstract class CardBase
    {
        /// <summary>
        /// The expiry month of the card
        /// </summary>
        /// [Required]
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The four-digit expiry year of the card
        /// </summary>
        /// [Required]
        public int ExpiryYear { get; set; }
    }
}