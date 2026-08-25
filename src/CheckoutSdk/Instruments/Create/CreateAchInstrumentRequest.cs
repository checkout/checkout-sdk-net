namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Store ACH account details.
    /// </summary>
    public class CreateAchInstrumentRequest : CreateInstrumentRequest
    {
        public CreateAchInstrumentRequest() : base(InstrumentType.Ach)
        {
        }

        /// <summary>
        /// The details of the ACH account.
        /// [Required]
        /// </summary>
        public CreateAchInstrumentData InstrumentData { get; set; }

        /// <summary>
        /// The account holder details.
        /// [Required]
        /// </summary>
        public CreateAchAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The customer's details.
        /// [Optional]
        /// </summary>
        public CreateCustomerInstrumentRequest Customer { get; set; }
    }
}
