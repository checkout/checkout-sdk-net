namespace Checkout.NetworkTokens.Common.Responses
{
    public class Card
    {
        /// <summary> The last four digits of the card number (Required, constraints: ^[0-9]{4}$) </summary>
        public string Last4 { get; set; }

        /// <summary> The card's expiration month (Required, constraints: [ 1 .. 12 ], ^[0-9]{1,2}$) </summary>
        public string ExpiryMonth { get; set; }

        /// <summary> The card's expiration year (Required, constraints: ^[0-9]{4}$) </summary>
        public string ExpiryYear { get; set; }
    }
}
