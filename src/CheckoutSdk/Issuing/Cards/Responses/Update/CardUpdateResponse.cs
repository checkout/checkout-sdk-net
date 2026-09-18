using System;
using Checkout.Common;

namespace Checkout.Issuing.Cards.Responses.Update
{
    /// <summary>
    /// The response returned when a card's details are updated.
    /// </summary>
    public class CardUpdateResponse : Resource
    {
        /// <summary>
        /// The date and time when the card was last modified, in UTC.
        /// [Required]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// The card's encrypted CVV, returned when the return-encrypted-cvv header is true.
        /// [Optional]
        /// </summary>
        public string EncryptedCvv { get; set; }
    }
}
