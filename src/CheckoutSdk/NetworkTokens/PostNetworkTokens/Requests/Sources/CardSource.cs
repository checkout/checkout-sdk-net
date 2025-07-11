namespace Checkout.NetworkTokens.PostNetworkTokens.Requests.Sources
{
    public class CardSource : AbstractSource
    {
        /// <summary>Initializes a new instance of the CardSource class.</summary>
        public CardSource() : base(SourceType.Card) { }

        /// <summary> The card number (Required, constraints: [ 12 .. 19 ] characters, ^[0-9]+$, [ 12 .. 19 ]) </summary>
        public string Number { get; set; }

        /// <summary> The expiry month of the card (Required, constraints: [ 1 .. 12 ], ^[0-9]{1,2}$) </summary>
        public string ExpiryMonth { get; set; }

        /// <summary> The four-digit expiry year of the card (Required, constraints: ^[0-9]{4}$) </summary>
        public string ExpiryYear { get; set; }

        /// <summary> The CVV number for the card (Optional, constraints: [ 1 .. 9999 ]) </summary>
        public string Cvv { get; set; }
    }
}
