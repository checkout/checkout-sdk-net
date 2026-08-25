using Checkout.Common;

namespace Checkout.Instruments.Update
{
    /// <summary>
    /// The details of the SEPA account being updated.
    /// </summary>
    public class UpdateSepaInstrumentData
    {
        /// <summary>
        /// The type of SEPA mandate.
        /// [Optional]
        /// </summary>
        public SepaMandateType? Type { get; set; }

        /// <summary>
        /// The IBAN of the SEPA account.
        /// [Required]
        /// min 15 characters, max 34 characters
        /// </summary>
        public string AccountNumber { get; set; }

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
        /// The type of payment. recurring or regular.
        /// [Required]
        /// </summary>
        public SepaPaymentType? PaymentType { get; set; }

        /// <summary>
        /// The identifier of the SEPA mandate.
        /// [Optional]
        /// min 1 characters, max 35 characters
        /// </summary>
        public string MandateId { get; set; }

        /// <summary>
        /// The date the mandate was signed.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public string DateOfSignature { get; set; }
    }
}
