namespace Checkout.Issuing.CardholderAccessTokens.Requests
{
    public class CardholderAccessTokenRequest
    {
        /// <summary>
        /// OAuth grant type.
        /// [Required]
        /// </summary>
        public string GrantType { get; set; }

        /// <summary>
        /// Access key ID.
        /// [Required]
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Access key secret.
        /// [Required]
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The cardholder's unique identifier.
        /// ^crh_[a-z0-9]{26}$
        /// 30 characters
        /// </summary>
        public string CardholderId { get; set; }

        /// <summary>
        /// Specifies if the request is for a single-use token.
        /// Single-use tokens are required for sensitive endpoints.
        /// </summary>
        public bool? SingleUse { get; set; }
    }
}