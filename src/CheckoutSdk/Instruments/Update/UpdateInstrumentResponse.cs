namespace Checkout.Instruments.Update
{
    /// <summary>
    /// The base response for PATCH /instruments/{id}.
    /// The concrete type is selected by the type discriminator: card, bank_account, sepa, ach or
    /// bacs.
    /// </summary>
    public class UpdateInstrumentResponse : HttpMetadata
    {
        /// <summary>
        /// The type of instrument.
        /// [Required] on every concrete variant. Not declared on the base schema.
        /// </summary>
        public InstrumentType? Type { get; set; }

        public UpdateInstrumentResponse(InstrumentType? type)
        {
            Type = type;
        }
    }
}
