using Newtonsoft.Json;

namespace Checkout.Issuing.Cards.Requests.Update
{
    /// <summary>
    /// The optional HTTP headers accepted when updating a card's details.
    /// </summary>
    public class CardUpdateHeaders : IHeaders
    {
        /// <summary>
        /// The <c>return-encrypted-cvv</c> HTTP header. Set to true to retrieve the card's
        /// encrypted credentials in the response. Requires an RSA public key to be provided in
        /// the <c>Encryption-Key</c> header.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "return-encrypted-cvv")]
        public bool? ReturnEncryptedCvv { get; set; }

        /// <summary>
        /// The <c>Encryption-Key</c> HTTP header. The RSA public key used to encrypt returned
        /// credentials. Required when the <c>return-encrypted-cvv</c> header is set to true.
        /// Provide the public key with the -----BEGIN PUBLIC KEY----- and -----END PUBLIC KEY-----
        /// headers and any newline characters removed, encoded as Base64.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "Encryption-Key")]
        public string EncryptionKey { get; set; }
    }
}
