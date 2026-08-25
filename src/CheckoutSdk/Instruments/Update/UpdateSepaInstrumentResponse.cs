namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Update SEPA account instrument response.
    /// The type and id are inherited from UpdateInstrumentResponse.
    /// </summary>
    public class UpdateSepaInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateSepaInstrumentResponse() : base(InstrumentType.Sepa)
        {
        }

        /// <summary>
        /// A token that can uniquely identify this instrument across all customers.
        /// [Required]
        /// Pattern: ^([a-z0-9]{26})$
        /// </summary>
        public string Fingerprint { get; set; }
    }
}
