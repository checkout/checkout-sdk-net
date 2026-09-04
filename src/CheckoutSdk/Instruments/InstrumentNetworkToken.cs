namespace Checkout.Instruments
{
    /// <summary>
    /// Network token details returned on a stored card instrument.
    /// </summary>
    public class InstrumentNetworkToken
    {
        /// <summary>
        /// The network token's unique identifier.
        /// [Required]
        /// Pattern: ^(nt)_(\w{26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The token status.
        /// [Optional]
        /// </summary>
        public InstrumentNetworkTokenState? State { get; set; }
    }
}
