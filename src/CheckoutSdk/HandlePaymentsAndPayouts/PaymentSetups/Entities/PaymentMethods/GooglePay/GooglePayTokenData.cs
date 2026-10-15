namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Google Pay payment token data, as returned by the Google Pay API.
    /// </summary>
    public class GooglePayTokenData
    {
        /// <summary>
        /// The encryption and signing scheme used to create this message. If not set, the default is ECv0.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string ProtocolVersion { get; set; }

        /// <summary>
        /// The signature that verifies the message came from Google, created using ECDSA.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// A serialized JSON string containing the encryptedMessage, ephemeralPublicKey, and tag. To simplify the
        /// signature verification process, this value is serialized.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string SignedMessage { get; set; }

        /// <summary>
        /// The public key provided to the Google API in the
        /// tokenizationSpecification.parameters.gatewayMerchantId field.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string TokenizationKey { get; set; }
    }
}
