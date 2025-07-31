namespace Checkout.Authentication.Standalone.Common.Responses.Card
{
    /// <summary>
    /// card
    /// Details related to the Session source. This property should always be in the response, unless a card source was
    /// used and communication with Checkout.com's Vault was not possible.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// The identifier of the card instrument.
        /// [Required]
        /// </summary>
        public string InstrumentId { get; set; }

        /// <summary>
        /// A token that can uniquely identify this card across all customers.
        /// [Required]
        /// </summary>
        public string Fingerprint { get; set; }

        /// <summary>
        /// Additional details for this card
        /// [Optional]
        /// </summary>
        public Metadata.Metadata Metadata { get; set; }
    }
}