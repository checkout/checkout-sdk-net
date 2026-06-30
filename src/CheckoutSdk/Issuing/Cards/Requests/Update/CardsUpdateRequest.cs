using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Requests.Update
{
    public class CardsUpdateRequest
    {
        /// <summary>
        /// Your reference.
        /// [Optional]
        /// &lt;= 256 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// User's metadata.
        /// [Optional]
        /// </summary>
        public CardMetadata Metadata { get; set; }

        /// <summary>
        /// The card's expiration month.
        /// [Optional]
        /// &gt;= 1
        /// &lt;= 12
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The card's expiration year.
        /// [Optional]
        /// min 4 characters, max 4 characters
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// ISO 8601 date scheduling the card's activation. Two formats are supported:
        /// - Date only: YYYY-MM-DD (treated as midnight UTC)
        /// - Date with round hour: YYYY-MM-DDTHH:mmZ (UTC) or YYYY-MM-DDTHH:mm±HH:mm (offset)
        /// Only round hours are allowed when a time is provided (HH:00). The value must be at least the next
        /// round hour after the request time.
        /// [Optional]
        /// </summary>
        public string ActivationDate { get; set; }

        /// <summary>
        /// Date for the card to be automatically revoked. Must be after the current date and date only in the
        /// form yyyy-mm-dd.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public string RevocationDate { get; set; }
    }
}