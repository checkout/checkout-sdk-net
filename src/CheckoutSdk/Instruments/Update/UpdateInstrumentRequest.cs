namespace Checkout.Instruments.Update
{
    /// <summary>
    /// The base request for updating a stored payment instrument via PATCH /instruments/{id}.
    /// The concrete type is selected by the type discriminator: card, bank_account, sepa, ach or
    /// bacs.
    /// </summary>
    public abstract class UpdateInstrumentRequest
    {
        /// <summary>
        /// The type of instrument.
        /// [Optional]
        /// </summary>
        public InstrumentType? Type { get; set; }

        protected UpdateInstrumentRequest(InstrumentType type)
        {
            Type = type;
        }
    }
}
