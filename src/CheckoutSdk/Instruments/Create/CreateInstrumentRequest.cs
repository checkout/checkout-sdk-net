namespace Checkout.Instruments.Create
{
    /// <summary>
    /// The base request for storing a payment instrument via POST /instruments.
    /// The concrete type is selected by the type discriminator: bank_account, card, token, sepa,
    /// ach or bacs.
    /// </summary>
    public abstract class CreateInstrumentRequest
    {
        /// <summary>
        /// The type of instrument.
        /// [Required]
        /// </summary>
        public InstrumentType? Type { get; set; }

        protected CreateInstrumentRequest(InstrumentType type)
        {
            Type = type;
        }
    }
}
