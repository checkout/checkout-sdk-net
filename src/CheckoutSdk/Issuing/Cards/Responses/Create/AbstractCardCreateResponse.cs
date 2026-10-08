using Checkout.Common;
using Checkout.Issuing.Common;
using System;

namespace Checkout.Issuing.Cards.Responses.Create
{
    /// <summary>
    /// The response returned when a card is created, discriminated on type (virtual or physical).
    /// _links (CardLinks: self, credentials, revoke, controls and more) is exposed through Links.
    /// </summary>
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
        /// 30 characters
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The client's unique identifier.
        /// [Required]
        /// ^cli_[a-z0-9]{26}$
        /// 30 characters
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The entity's unique identifier.
        /// [Required]
        /// ^ent_[a-z0-9]{26}$
        /// 30 characters
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
        /// 4 characters
        /// </summary>
        public string LastFour { get; set; }

        /// <summary>
        /// The card's expiration month.
        /// [Required]
        /// Format: int32
        /// min 1, max 12
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The card's expiration year.
        /// [Required]
        /// Format: int32
        /// 4 digits
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// The issuing currency, as a three-letter ISO currency code.
        /// [Required]
        /// Format: ISO4217
        /// ^[a-zA-Z]{3}$
        /// 3 characters
        /// </summary>
        public Currency? BillingCurrency { get; set; }

        /// <summary>
        /// The issuing country, as a two-letter ISO country code.
        /// [Required]
        /// 2 characters
        /// </summary>
        public CountryCode? IssuingCountry { get; set; }

        /// <summary>
        /// The card scheme.
        /// [Required]
        /// </summary>
        public IssuingScheme? Scheme { get; set; }

        /// <summary>
        /// The date and time the card was created.
        /// [Required]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// The card's status, which determines whether it can approve incoming authorizations.
        /// inactive: the default state, authorizations are declined until the card is activated.
        /// active: authorization requests can be approved.
        /// suspended: authorization requests are declined; the card can be reactivated.
        /// revoked: authorization requests are permanently declined; the card cannot be reactivated.
        /// [Required]
        /// </summary>
        public CardStatus? Status { get; set; }

        /// <summary>
        /// Your reference.
        /// [Optional]
        /// max 256 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The card will be revoked at midnight UTC on the date specified.
        /// Not listed in add-card-response, but sent on add-card-request and returned by get-card-response.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public string ScheduledRevocationDate { get; set; }

        /// <summary>
        /// The date and time the card was last activated. If the card has never been activated, this field
        /// returns null.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// Nullable, read-only
        /// </summary>
        public DateTime? LastActivatedOn { get; set; }

        /// <summary>
        /// Initializes the card response with its discriminator value.
        /// </summary>
        /// <param name="type">The card type.</param>
        protected AbstractCardCreateResponse(IssuingCardType? type)
        {
            Type = type;
        }
    }
}
