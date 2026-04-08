using Checkout.Common;
using System;

namespace Checkout.Issuing.DigitalCards.Responses
{
    /// <summary>
    /// Response containing the details of a digital card.
    /// </summary>
    public class GetDigitalCardResponse : Resource
    {
        /// <summary>
        /// The digital card's unique identifier.
        /// [Required]
        /// Pattern: ^dcr_[a-z0-9]{26}$, length 30 characters.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The card's unique identifier.
        /// [Required]
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// The client's unique identifier.
        /// [Required]
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The entity's unique identifier.
        /// [Required]
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The last four digits of the card number.
        /// [Required]
        /// </summary>
        public string LastFour { get; set; }

        /// <summary>
        /// The digital card's status.
        /// [Required]
        /// </summary>
        public IssuingDigitalCardStatus? Status { get; set; }

        /// <summary>
        /// The type of digital card.
        /// [Optional]
        /// Enum: "secure_element" "host_card_emulation" "card_on_file" "e_commerce" "qr_code"
        /// </summary>
        public IssuingDigitalCardType? Type { get; set; }

        /// <summary>
        /// The scheme card identifier.
        /// [Optional]
        /// </summary>
        public string SchemeCardId { get; set; }

        /// <summary>
        /// The requestor details.
        /// [Optional]
        /// </summary>
        public IssuingDigitalCardRequestor Requestor { get; set; }

        /// <summary>
        /// The device details.
        /// [Optional]
        /// </summary>
        public IssuingDigitalCardDevice Device { get; set; }

        /// <summary>
        /// The date and time the digital card was provisioned.
        /// [Optional]
        /// Format: date-time
        /// </summary>
        public DateTime? ProvisionedOn { get; set; }
    }
}
