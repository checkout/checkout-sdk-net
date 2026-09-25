using Checkout.Common;
using Checkout.Issuing.Common;
using System;

namespace Checkout.Issuing.Cards.Responses.Create
{
    public abstract class AbstractCardCreateResponse : Resource
    {
        /// <summary>
        /// The card type.
        /// [Required]
        /// </summary>
        public IssuingCardType? Type { get; set; }

        /// <summary>
        /// The card's unique identifier.
        /// [Required]
        /// ^crd_[a-z0-9]{26}$
        /// min 30 characters, max 30 characters
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The client's unique identifier.
        /// [Required]
        /// ^cli_[a-z0-9]{26}$
        /// min 30 characters, max 30 characters
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The entity's unique identifier.
        /// [Required]
        /// ^ent_[a-z0-9]{26}$
        /// min 30 characters, max 30 characters
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The name to display on the card.
        /// [Optional]
        /// ^[0-9a-zA-Z.\- ]{2,26}$
        /// min 2 characters, max 26 characters
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The last four digits of the card number, also known as the PAN.
        /// [Required]
        /// ^[0-9]{4}$
        /// </summary>
        public string LastFour { get; set; }

        /// <summary>
        /// The card's expiration month.
        /// [Required]
        /// &gt;= 1
        /// &lt;= 12
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The card's expiration year.
        /// [Required]
        /// min 4 characters, max 4 characters
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// The issuing currency, as a three-letter ISO currency code.
        /// [Required]
        /// ^[a-zA-Z]{3}$
        /// min 3 characters, max 3 characters
        /// </summary>
        public Currency? BillingCurrency { get; set; }

        /// <summary>
        /// The issuing country, as a two-letter ISO country code.
        /// [Required]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? IssuingCountry { get; set; }

        /// <summary>
        /// The date and time the card was created, in UTC.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// The card's status, which determines whether it can approve incoming authorizations.
        /// [Required]
        /// Enum: "active" "inactive" "revoked" "suspended"
        /// </summary>
        public CardStatus? Status { get; set; }

        /// <summary>
        /// Your reference.
        /// [Optional]
        /// &lt;= 256 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The card will be revoked at midnight UTC on the date specified.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public string ScheduledRevocationDate { get; set; }

        /// <summary>
        /// The date and time the card was last activated. If the card has never been activated, this field
        /// returns null.
        /// [Optional, read-only]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? LastActivatedOn { get; set; }

        protected AbstractCardCreateResponse(IssuingCardType? type)
        {
            Type = type;
        }
    }
}