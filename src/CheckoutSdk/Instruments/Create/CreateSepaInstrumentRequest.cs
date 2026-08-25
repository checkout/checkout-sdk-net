namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Store SEPA account details.
    /// </summary>
    public class CreateSepaInstrumentRequest : CreateInstrumentRequest
    {
        public CreateSepaInstrumentRequest() : base(InstrumentType.Sepa)
        {
        }

        /// <summary>
        /// The details of the SEPA account.
        /// [Required]
        /// </summary>
        public CreateSepaInstrumentData InstrumentData { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Required]
        /// </summary>
        public CreateSepaAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The customer's details.
        /// [Optional]
        /// </summary>
        public CreateCustomerInstrumentRequest Customer { get; set; }
    }
}
