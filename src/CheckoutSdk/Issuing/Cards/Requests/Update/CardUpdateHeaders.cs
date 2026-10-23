using System;
using Newtonsoft.Json;

namespace Checkout.Issuing.Cards.Requests.Update
{
    /// <summary>
    /// The return-encrypted-cvv and Encryption-Key HTTP headers for updating a card's details.
    /// The API no longer documents these headers and update-card-response does not carry an
    /// encrypted CVV. They are still sent when set. Will be removed in the next major version.
    /// </summary>
    [Obsolete(DeprecationMessage)]
    public class CardUpdateHeaders : IHeaders
    {
        /// <summary>
        /// The deprecation message shared by this class and the UpdateCardDetails overload that
        /// takes it.
        /// </summary>
        internal const string DeprecationMessage =
            "The API no longer supports these headers; use the method without headers. Will be removed in the next major version.";

        /// <summary>
        /// The <c>return-encrypted-cvv</c> HTTP header, sent as "true" or "false".
        /// [Optional]
        /// [Deprecated] No longer documented by the API, and update-card-response does not carry
        /// an encrypted CVV. Still sent when set. Will be removed in the next major version.
        /// </summary>
        [JsonProperty(PropertyName = "return-encrypted-cvv")]
        public bool? ReturnEncryptedCvv { get; set; }

        /// <summary>
        /// The <c>Encryption-Key</c> HTTP header.
        /// [Optional]
        /// [Deprecated] No longer documented by the API. Still sent when set. Will be removed in
        /// the next major version.
        /// </summary>
        [JsonProperty(PropertyName = "Encryption-Key")]
        public string EncryptionKey { get; set; }
    }
}
