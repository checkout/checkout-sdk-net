using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// SEPA Direct Debit source.
    /// </summary>
    public class RequestSepaSource : AbstractRequestSource
    {
        public RequestSepaSource() : base(PaymentSourceType.Sepa)
        {
        }

        /// <summary>
        /// The account's country, as an ISO 3166-1 alpha-2 code.
        /// [Required]
        /// </summary>
        public CountryCode? Country { get; set; }

        /// <summary>
        /// The account holder's IBAN.
        /// [Required]
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Not declared by PaymentRequestSEPAV4Source. No SEPA schema in the specification declares a
        /// bank code, and the SEPA source is identified by IBAN through AccountNumber. Retained
        /// for retro-compatibility purposes only. Possibly an obsoleted field.
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// The account holder's account currency.
        /// [Required]
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The ID of the mandate.
        /// [Optional]
        /// </summary>
        public string MandateId { get; set; }

        /// <summary>
        /// The type of mandate.
        /// [Optional]
        /// </summary>
        public SepaMandateType? MandateType { get; set; }

        /// <summary>
        /// The date the mandate was signed, in the format yyyy-MM-dd.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public string DateOfSignature { get; set; }

        /// <summary>
        /// The account holder's personal information.
        /// [Required]
        /// </summary>
        public SepaSourceAccountHolder AccountHolder { get; set; }
    }
}
