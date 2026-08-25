namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Update ACH account details.
    /// </summary>
    public class UpdateAchInstrumentRequest : UpdateInstrumentRequest
    {
        public UpdateAchInstrumentRequest() : base(InstrumentType.Ach)
        {
        }

        /// <summary>
        /// The details of the ACH account.
        /// [Optional]
        /// </summary>
        public UpdateAchInstrumentData InstrumentData { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Optional]
        /// </summary>
        public UpdateAchAccountHolder AccountHolder { get; set; }
    }
}
