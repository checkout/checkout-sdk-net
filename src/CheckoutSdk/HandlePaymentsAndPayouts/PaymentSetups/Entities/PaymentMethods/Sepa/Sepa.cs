using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The SEPA payment method's details and configuration.
    /// </summary>
    public class Sepa : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The account holder details.
        /// [Optional]
        /// </summary>
        public SepaAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The account holder's IBAN.
        /// [Optional] writeOnly
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The account's country, as an ISO 3166-1 alpha-2 code.
        /// [Optional]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }

        /// <summary>
        /// The account holder's account currency.
        /// [Optional]
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The mandate details.
        /// [Optional]
        /// </summary>
        public SepaMandate Mandate { get; set; }
    }
}
