using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Checkout.com token for a wallet payment method (Apple Pay or Google Pay).
    /// </summary>
    public class WalletToken
    {
        /// <summary>
        /// The token identifier, prefixed by tok_.
        /// [Required]
        /// ^(tok)_(\w{26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The date and time the token expires, in ISO 8601 format.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? ExpiresOn { get; set; }
    }
}
