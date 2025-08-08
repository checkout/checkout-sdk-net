namespace Checkout.Authentication.Standalone.Common.Responses.GoogleSpa.Token
{
    /// <summary>
    /// token
    /// Token for the given PAN provisioned and authenticated
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Value of token. Represented as a numerical string
        /// [Optional]
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Expiry month of the token
        /// [Optional]
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// Expiry year of the token
        /// [Optional]
        /// </summary>
        public int? ExpiryYear { get; set; }
    }
}