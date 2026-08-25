namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Update SEPA account details.
    /// </summary>
    public class UpdateSepaInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateSepaInstrumentRequest() : base(InstrumentType.Sepa)
        {
        }

        /// <summary>
        /// The details of the SEPA account.
        /// [Optional]
        /// </summary>
        public UpdateSepaInstrumentData InstrumentData { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Optional]
        /// </summary>
        public UpdateSepaAccountHolder AccountHolder { get; set; }
    }
}
