using System;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// SEPA instrument response.
    /// The type, id, fingerprint and customer are inherited from GetInstrumentResponse.
    /// </summary>
    public class GetSepaInstrumentResponse : GetInstrumentResponse
    {
        public GetSepaInstrumentResponse() : base(InstrumentType.Sepa)
        {
        }

        /// <summary>
        /// The date and time the instrument was created.
        /// [Required]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// The date and time the instrument was last modified.
        /// [Required]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// The Vault ID currently attached to the instrument.
        /// [Required]
        /// </summary>
        public string VaultId { get; set; }

        /// <summary>
        /// The details of the SEPA account.
        /// [Optional]
        /// </summary>
        public GetSepaInstrumentData InstrumentData { get; set; }

        /// <summary>
        /// The account holder details.
        /// This hides GetInstrumentResponse.AccountHolder, whose shared type carries fields the SEPA
        /// schema does not declare. Cast the response to GetSepaInstrumentResponse to read it: the
        /// hidden base property stays null for a SEPA instrument.
        /// [Optional]
        /// </summary>
        public new GetSepaAccountHolder AccountHolder { get; set; }
    }
}
