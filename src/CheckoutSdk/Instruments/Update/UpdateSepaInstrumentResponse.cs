namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Update SEPA account instrument response.
    /// The type is inherited from UpdateInstrumentResponse. The id is declared per
    /// variant: the sepa, ach and bacs update responses have one, card and
    /// bank_account do not.
    /// </summary>
    public class UpdateSepaInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateSepaInstrumentResponse() : base(InstrumentType.Sepa)
        {
        }

        /// <summary>
        /// The unique identifier of the payment source or destination that can be used later for
        /// payments.
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A token that can uniquely identify this instrument across all customers.
        /// [Required]
        /// Pattern: ^([a-z0-9]{26})$
        /// </summary>
        public string Fingerprint { get; set; }
    }
}
