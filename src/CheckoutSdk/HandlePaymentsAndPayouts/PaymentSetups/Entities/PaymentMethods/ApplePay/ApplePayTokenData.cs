namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Apple Pay payment token data, as returned by Apple Pay on the device.
    /// </summary>
    public class ApplePayTokenData
    {
        /// <summary>
        /// The version of the payment token. The token uses EC_v1 for ECC-encrypted data, and RSA_v1 for
        /// RSA-encrypted data.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The encrypted payment data, Base64-encoded.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// The signature of the payment and header data. The signature includes the signing certificate, its
        /// intermediate CA certificate, and information about the signing algorithm.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Additional version-dependent information used to decrypt and verify the payment.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public ApplePayTokenDataHeader Header { get; set; }
    }
}
