namespace Checkout.Instruments
{
    /// <summary>
    /// The updated payment instrument's response.
    /// </summary>
    public class UpdateInstrumentResponse
    {
        /// <summary>
        /// The instrument type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// A token that can uniquely identify this card across all customers.
        /// </summary>
        public string Fingerprint { get; set; }
    }
}
