using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Bacs payment method's details and configuration.
    /// </summary>
    public class Bacs : PaymentMethodBase
    {
        /// <summary>
        /// The ID of the Bacs instrument used for the payment.
        /// [Optional] readOnly
        /// </summary>
        public string InstrumentId { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Optional]
        /// </summary>
        public BacsAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The account number of the Bacs Direct Debit account.
        /// [Optional] writeOnly
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The sort code of the Bacs Direct Debit account.
        /// [Optional] writeOnly
        /// </summary>
        public string BankCode { get; set; }

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
        /// Whether vault may accept a partial match when looking up an existing Bacs instrument for the
        /// supplied account details.
        /// [Optional]
        /// Default: false
        /// </summary>
        public bool? AllowPartialMatch { get; set; }
    }
}
