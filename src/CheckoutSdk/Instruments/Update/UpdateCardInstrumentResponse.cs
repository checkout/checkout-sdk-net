namespace Checkout.Instruments.Update
{
    /// <summary>
    /// The response returned after updating a stored card instrument.
    /// </summary>
    public class UpdateCardInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateCardInstrumentResponse() : base(InstrumentType.Card)
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
