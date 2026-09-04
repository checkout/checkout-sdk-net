using Checkout.Common;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The details of a stored Bacs Direct Debit account.
    /// </summary>
    public class GetBacsInstrumentData
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
        /// The type of payment.
        /// [Required]
        /// </summary>
        public BacsPaymentType? PaymentType { get; set; }

        /// <summary>
        /// Whether vault accepted a partial match when looking up the Bacs instrument for the
        /// supplied account details.
        /// [Optional]
        /// </summary>
        public bool? AllowPartialMatch { get; set; }

        /// <summary>
        /// The validation status of the account.
        /// [Optional]
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The result of matching the account holder name against the account owner.
        /// [Optional]
        /// </summary>
        public string MatchStatus { get; set; }

        /// <summary>
        /// A human-readable description of the validation result.
        /// [Optional]
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The identifier of the Bacs Direct Debit mandate.
        /// [Optional]
        /// </summary>
        public string MandateId { get; set; }
    }
}
