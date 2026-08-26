using Checkout.Common;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The base response for GET /instruments/{id}.
    /// The concrete type is selected by the type discriminator: bank_account, card, sepa, ach or
    /// bacs.
    /// </summary>
    public class GetInstrumentResponse : HttpMetadata
    {
        public GetInstrumentResponse()
        {
        }

        public GetInstrumentResponse(InstrumentType? type)
        {
            Type = type;
        }

        /// <summary>
        /// The underlying instrument type.
        /// [Required]
        /// </summary>
        public InstrumentType? Type { get; set; }

        /// <summary>
        /// The unique identifier of the payment source or destination that can be used later for
        /// payments.
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A token that can uniquely identify this instrument across all customers.
        /// [Required] on every concrete variant. Not declared on the base schema.
        /// Pattern: ^([a-z0-9]{26})$
        /// </summary>
        public string Fingerprint { get; set; }

        /// <summary>
        /// The stored customer details.
        /// [Optional]
        /// </summary>
        public InstrumentCustomerResponse Customer { get; set; }

        /// <summary>
        /// The account holder details. The SEPA, Bacs and ACH variants hide this member with a
        /// scheme-specific type, because the shared type carries fields those schemas do not
        /// declare.
        /// [Optional]
        /// </summary>
        public AccountHolder AccountHolder { get; set; }
    }
}
