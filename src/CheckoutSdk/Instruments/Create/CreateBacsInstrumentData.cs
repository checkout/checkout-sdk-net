using Checkout.Common;

namespace Checkout.Instruments.Create
{
    /// <summary>
    /// The details of the Bacs Direct Debit account being stored.
    /// </summary>
    public class CreateBacsInstrumentData
    {
        /// <summary>
        /// The account number of the Bacs Direct Debit account.
        /// [Required]
        /// min 8 characters, max 8 characters
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The sort code of the Bacs Direct Debit account.
        /// [Required]
        /// min 6 characters, max 6 characters
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// The country of the account, as an ISO 3166-1 alpha-2 code.
        /// [Required]
        /// min 2 characters, max 2 characters
        /// </summary>
        public CountryCode? Country { get; set; }

        /// <summary>
        /// The currency of the account.
        /// [Required]
        /// min 3 characters, max 3 characters
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The type of payment. Recurring or Regular.
        /// [Required]
        /// </summary>
        public BacsPaymentType? PaymentType { get; set; }

        /// <summary>
        /// Indicates whether the Bacs instrument is created when account validation returns a partial
        /// match. When true, the instrument is created on a partial match; when false, instrument
        /// creation fails on a partial match.
        /// [Optional]
        /// Default: false
        /// </summary>
        public bool? AllowPartialMatch { get; set; }
    }
}
