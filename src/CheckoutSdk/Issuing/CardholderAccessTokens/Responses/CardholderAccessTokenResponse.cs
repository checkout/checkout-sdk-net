namespace Checkout.Issuing.CardholderAccessTokens.Responses
{
    public class CardholderAccessTokenResponse : HttpMetadata
    {
        /// <summary>
        /// [Required]
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// [Required]
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// The remaining time the access token is valid for, in seconds.
        /// </summary>
        public long? ExpiresIn { get; set; }

        /// <summary>
        /// </summary>
        public string Scope { get; set; }
    }
}