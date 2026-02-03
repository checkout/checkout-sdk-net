namespace Checkout.StandaloneAccountUpdater.Requests
{
    public class CardDetails
    {
        /// <summary>
        /// The card number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The expiry month of the card
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The four-digit expiry year of the card
        /// </summary>
        public int ExpiryYear { get; set; }
    }
}