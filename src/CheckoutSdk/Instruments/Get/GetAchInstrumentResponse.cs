using System;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// ACH instrument response.
    /// The type, id, fingerprint and customer are inherited from GetInstrumentResponse.
    /// </summary>
    public class GetAchInstrumentResponse : GetInstrumentResponse
    {
        public GetAchInstrumentResponse() : base(InstrumentType.Ach)
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
        /// The details of the ACH account.
        /// [Required]
        /// </summary>
        public GetAchInstrumentData InstrumentData { get; set; }

        /// <summary>
        /// The account holder details.
        /// This hides GetInstrumentResponse.AccountHolder, whose shared type carries fields the ACH
        /// schema does not declare. Cast the response to GetAchInstrumentResponse to read it: the
        /// hidden base property stays null for an ACH instrument.
        /// [Required]
        /// </summary>
        public new GetAchAccountHolder AccountHolder { get; set; }
    }
}
