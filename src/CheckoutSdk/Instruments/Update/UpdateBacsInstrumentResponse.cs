namespace Checkout.Instruments.Update
{
    /// <summary>
    /// Update Bacs Direct Debit account instrument response.
    /// The type and id are inherited from UpdateInstrumentResponse.
    /// </summary>
    public class UpdateBacsInstrumentResponse : UpdateInstrumentResponse
    {
        public UpdateBacsInstrumentResponse() : base(InstrumentType.Bacs)
        {
        }

        /// <summary>
        /// The unique identifier of the payment source or destination that can be used later for
        /// payments.
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A token that can uniquely identify this instrument across all customers.
        /// [Required]
        /// Pattern: ^([a-z0-9]{26})$
        /// </summary>
        public string Fingerprint { get; set; }
    }
}
