namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Update Bacs Direct Debit account details.
    /// </summary>
    public class UpdateBacsInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateBacsInstrumentRequest() : base(InstrumentType.Bacs)
        {
        }

        /// <summary>
        /// The details of the Bacs Direct Debit account.
        /// [Optional]
        /// </summary>
        public UpdateBacsInstrumentData InstrumentData { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Optional]
        /// </summary>
        public UpdateBacsAccountHolder AccountHolder { get; set; }
    }
}
