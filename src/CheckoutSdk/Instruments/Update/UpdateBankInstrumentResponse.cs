namespace Checkout.Instruments.Update
{
    /// <summary>
    /// The response returned after updating a stored bank account instrument.
    /// </summary>
    public class UpdateBankInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateBankInstrumentResponse() : base(InstrumentType.BankAccount)
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
