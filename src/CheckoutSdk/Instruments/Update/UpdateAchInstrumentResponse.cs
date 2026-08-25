namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Update ACH account instrument response.
    /// The type and id are inherited from UpdateInstrumentResponse.
    /// </summary>
    public class UpdateAchInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateAchInstrumentResponse() : base(InstrumentType.Ach)
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
