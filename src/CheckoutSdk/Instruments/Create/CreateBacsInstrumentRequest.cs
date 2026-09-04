namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Store Bacs Direct Debit account details.
    /// </summary>
    public class CreateBacsInstrumentRequest : CreateInstrumentRequest
    {
        public CreateBacsInstrumentRequest() : base(InstrumentType.Bacs)
        {
        }

        /// <summary>
        /// The account configuration for the instrument.
        /// [Required]
        /// </summary>
        public CreateBacsInstrumentAccount Account { get; set; }

        /// <summary>
        /// The details of the Bacs Direct Debit account.
        /// [Required]
        /// </summary>
        public CreateBacsInstrumentData InstrumentData { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Required]
        /// </summary>
        public CreateBacsAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The customer's details.
        /// [Optional]
        /// </summary>
        public CreateCustomerInstrumentRequest Customer { get; set; }
    }
}
