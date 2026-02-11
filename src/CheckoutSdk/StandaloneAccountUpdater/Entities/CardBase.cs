namespace Checkout.StandaloneAccountUpdater.Entities
{
    /// <summary>
    /// Base class for card expiry date information
    /// </summary>
    public abstract class CardBase
    {
        /// <summary>
        /// The expiry month of the card
        /// [Required]
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The four-digit expiry year of the card
        /// [Required]
        /// </summary>
        public int ExpiryYear { get; set; }
    }
}