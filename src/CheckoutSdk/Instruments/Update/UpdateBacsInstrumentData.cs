using Checkout.Common;

namespace Checkout.Instruments.Update
{
    /// <summary>
    /// The details of the Bacs Direct Debit account being updated.
    /// </summary>
    public class UpdateBacsInstrumentData
    {
        /// <summary>
        /// The account number of the Bacs Direct Debit account.
        /// [Optional]
        /// min 8 characters, max 8 characters
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The sort code of the Bacs Direct Debit account.
        /// [Optional]
        /// min 6 characters, max 6 characters
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// The country of the account, as an ISO 3166-1 alpha-2 code.
        /// [Optional]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }

        /// <summary>
        /// The currency of the account.
        /// [Optional]
        /// min 3 characters, max 3 characters
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The type of payment. Recurring or Regular.
        /// [Optional]
        /// </summary>
        public BacsPaymentType? PaymentType { get; set; }

        /// <summary>
        /// Whether vault accepted a partial match when looking up the Bacs instrument for the
        /// supplied account details.
        /// [Optional]
        /// </summary>
        public bool? AllowPartialMatch { get; set; }
    }
}
