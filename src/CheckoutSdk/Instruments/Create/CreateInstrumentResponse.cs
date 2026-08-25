namespace Checkout.Instruments.Create
{
    /// <summary>
    /// The response for the type of instrument stored.
    /// Every store variant declares a fingerprint, so it is carried here. The customer is declared
    /// only by the bank_account and card variants, so it lives on those types instead.
    /// </summary>
    public class CreateInstrumentResponse : HttpMetadata
    {
        /// <summary>
        /// The type of instrument.
        /// [Required]
        /// </summary>
        public InstrumentType? Type { get; set; }

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

        public CreateInstrumentResponse(InstrumentType? type)
        {
            Type = type;
        }
    }
}
