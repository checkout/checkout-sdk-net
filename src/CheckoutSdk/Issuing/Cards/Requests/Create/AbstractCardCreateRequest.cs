using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Requests.Create
{
    public abstract class AbstractCardCreateRequest
    {
        /// <summary>
        /// The card type.
        /// [Required]
        /// </summary>
        public IssuingCardType? Type { get; set; }

        /// <summary>
        /// The cardholder's unique identifier.
        /// [Required]
        /// ^crh_[a-z0-9]{26}$
        /// min 30 characters, max 30 characters
        /// </summary>
        public string CardholderId { get; set; }

        /// <summary>
        /// The card product's unique identifier. This field is required if there are multiple card products
        /// associated with the entity.
        /// [Required]
        /// </summary>
        public string CardProductId { get; set; }

        /// <summary>
        /// The duration of time during which the card will accept incoming authorizations. The unit and value
        /// combination you supply will determine the card's expiry date.
        /// [Optional]
        /// </summary>
        public CardLifetime Lifetime { get; set; }

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
        /// Date for the card to be automatically revoked. Must be after the current date and date only in the
        /// form yyyy-mm-dd.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public string RevocationDate { get; set; }

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
        /// The name to display on the card.
        /// [Optional]
        /// ^[0-9a-zA-Z.\- ]{2,26}$
        /// min 2 characters, max 26 characters
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Sets whether to activate the newly created card upon creation. If set to false, the cardholder will
        /// not be able to process transactions until you activate the card.
        /// [Optional]
        /// Default: true for virtual cards, false for physical cards
        /// </summary>
        public bool ActivateCard { get; set; }

        protected AbstractCardCreateRequest(IssuingCardType type)
        {
            Type = type;
        }
    }
}