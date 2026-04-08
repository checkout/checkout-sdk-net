using Checkout.AgenticCommerce.Entities;
using System.Collections.Generic;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// The card details for a delegated payment.
    /// </summary>
    public class DelegatedPaymentMethodCard
    {
        /// <summary>
        /// The payment method type.
        /// [Required]
        /// </summary>
        public DelegatedPaymentMethodType Type { get; set; } = DelegatedPaymentMethodType.Card;

        /// <summary>
        /// The type of card number provided.
        /// [Required]
        /// </summary>
        public DelegatedCardNumberType CardNumberType { get; set; }

        /// <summary>
        /// The full card number.
        /// [Required]
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The card expiry month, in two-digit format (MM).
        /// [Optional]
        /// 2 characters.
        /// </summary>
        public string ExpMonth { get; set; }

        /// <summary>
        /// The card expiry year, in four-digit format (YYYY).
        /// [Optional]
        /// 4 characters.
        /// </summary>
        public string ExpYear { get; set; }

        /// <summary>
        /// The name of the cardholder as it appears on the card.
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The card verification code (CVC/CVV).
        /// [Optional]
        /// 3-4 characters.
        /// </summary>
        public string Cvc { get; set; }

        /// <summary>
        /// The cryptogram associated with a network token transaction.
        /// Required when <see cref="CardNumberType"/> is <see cref="DelegatedCardNumberType.NetworkToken"/>.
        /// [Optional]
        /// </summary>
        public string Cryptogram { get; set; }

        /// <summary>
        /// The Electronic Commerce Indicator (ECI) or Security Level Indicator (SLI) value
        /// for network token transactions.
        /// [Optional]
        /// </summary>
        public string EciValue { get; set; }

        /// <summary>
        /// A list of verification checks that have been performed on the card.
        /// [Optional]
        /// </summary>
        public IList<string> ChecksPerformed { get; set; }

        /// <summary>
        /// The Issuer Identification Number (IIN), also known as the Bank Identification Number (BIN).
        /// Typically the first 6 digits of the card number.
        /// [Optional]
        /// 6 characters.
        /// </summary>
        public string Iin { get; set; }

        /// <summary>
        /// The funding type of the card, used for display purposes.
        /// [Optional]
        /// </summary>
        public DelegatedCardFundingType? DisplayCardFundingType { get; set; }

        /// <summary>
        /// The wallet type associated with the card, used for display purposes
        /// (for example, Apple Pay or Google Pay).
        /// [Optional]
        /// </summary>
        public string DisplayWalletType { get; set; }

        /// <summary>
        /// The card brand, used for display purposes (for example, Visa, Mastercard).
        /// [Optional]
        /// </summary>
        public string DisplayBrand { get; set; }

        /// <summary>
        /// The last four digits of the card number, used for display purposes.
        /// [Optional]
        /// 4 characters.
        /// </summary>
        public string DisplayLast4 { get; set; }

        /// <summary>
        /// A set of key-value pairs containing additional payment method metadata.
        /// [Required]
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; }
    }
}
