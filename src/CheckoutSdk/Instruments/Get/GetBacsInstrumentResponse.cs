using System;
using System.Collections.Generic;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// Bacs Direct Debit instrument response.
    /// The type, id and fingerprint are inherited from GetInstrumentResponse.
    /// </summary>
    public class GetBacsInstrumentResponse : GetInstrumentResponse
    {
        public GetBacsInstrumentResponse() : base(InstrumentType.Bacs)
        {
        }

        /// <summary>
        /// The date and time the instrument was created.
        /// [Required]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// The Vault ID currently attached to the instrument.
        /// [Required]
        /// </summary>
        public string VaultId { get; set; }

        /// <summary>
        /// The date and time the instrument was last modified.
        /// [Optional]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// The account configuration for the instrument.
        /// [Optional]
        /// </summary>
        public GetBacsInstrumentAccount Account { get; set; }

        /// <summary>
        /// The list of validations performed on the instrument.
        /// The API does not publish an item schema for this array, so each entry is exposed as an
        /// untyped map.
        /// [Optional]
        /// </summary>
        public IList<IDictionary<string, object>> Validations { get; set; }

        /// <summary>
        /// The details of the Bacs Direct Debit account.
        /// [Optional]
        /// </summary>
        public GetBacsInstrumentData InstrumentData { get; set; }

        /// <summary>
        /// The account holder's details.
        /// This hides GetInstrumentResponse.AccountHolder, whose shared type carries fields the Bacs
        /// schema does not declare. Cast the response to GetBacsInstrumentResponse to read it: the
        /// hidden base property stays null for a Bacs instrument.
        /// [Optional]
        /// </summary>
        public new GetBacsAccountHolder AccountHolder { get; set; }
    }
}
