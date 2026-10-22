using System;
using Newtonsoft.Json;

namespace Checkout.Issuing.Cards.Requests.Update
{
    /// <summary>
    /// The return-encrypted-cvv and Encryption-Key HTTP headers for updating a card's details.
    /// The API no longer documents these headers and update-card-response does not carry an
    /// encrypted CVV. The API ignores them; they are still sent when set. Will be removed in the
    /// next major version.
    /// </summary>
    [Obsolete("The API no longer supports these headers; use the method without headers. Will be removed in the next major version.")]
    public class CardUpdateHeaders : IHeaders
    {
        /// <summary>
        /// The <c>return-encrypted-cvv</c> HTTP header, sent as "true" or "false".
        /// [Deprecated] The API no longer documents this header and ignores it: setting it does
        /// not add an encrypted CVV to the response. Still sent when set. Will be removed in the
        /// next major version.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "return-encrypted-cvv")]
        public bool? ReturnEncryptedCvv { get; set; }

        /// <summary>
        /// The <c>Encryption-Key</c> HTTP header.
        /// [Deprecated] The API no longer documents this header and ignores it. Still sent when
        /// set. Will be removed in the next major version.
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "Encryption-Key")]
        public string EncryptionKey { get; set; }
    }
}
