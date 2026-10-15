using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The card details for the card payment method. The full card number, the cardholder's name, the CVV and
    /// the stored indicator are only sent; the last four digits, the BIN, the scheme and the expiry of the
    /// details are only returned.
    /// </summary>
    public class CardDetails
    {
        /// <summary>
        /// The card number (without separators).
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The last four digits of the card number.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Last4 { get; set; }

        /// <summary>
        /// The card issuer's Bank Identification Number (BIN).
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Bin { get; set; }

        /// <summary>
        /// The card scheme.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// The expiry month of the card.
        /// [Optional]
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The expiry year of the card.
        /// [Optional]
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// The cardholder's name.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The card verification value/code. 3 digits, except for American Express (4 digits).
        /// [Optional]
        /// writeOnly
        /// </summary>
        public string Cvv { get; set; }

        /// <summary>
        /// Indicates whether this card is being submitted from your own stored card-on-file system, rather than
        /// being entered by the customer at the time of payment. Set to true when you provide card details that
        /// you already have on file for the customer.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public bool? Stored { get; set; }

        /// <summary>
        /// The time by which the card details must be confirmed.
        /// [Optional]
        /// readOnly
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// Indicates whether to use the Real-Time Account Updater to update the card information.
        /// [Optional]
        /// </summary>
        public bool? AllowUpdate { get; set; }
    }
}
