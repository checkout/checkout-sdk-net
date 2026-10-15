namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Additional version-dependent information used to decrypt and verify the Apple Pay payment.
    /// </summary>
    public class ApplePayTokenDataHeader
    {
        /// <summary>
        /// The ephemeral public key from the header of the Apple Pay payment token.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string EphemeralPublicKey { get; set; }

        /// <summary>
        /// The public key hash from the header of the Apple Pay payment token.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string PublicKeyHash { get; set; }

        /// <summary>
        /// The transaction identifier from the header of the Apple Pay payment token.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string TransactionId { get; set; }
    }
}
