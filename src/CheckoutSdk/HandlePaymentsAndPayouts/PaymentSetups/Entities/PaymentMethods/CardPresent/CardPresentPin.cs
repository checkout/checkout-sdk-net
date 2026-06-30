namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The encrypted PIN block details.
    /// </summary>
    public class CardPresentPin
    {
        /// <summary>
        /// The identifier of the key set used to encrypt the PIN block.
        /// [Required] writeOnly
        /// </summary>
        public string KeySetId { get; set; }

        /// <summary>
        /// The encrypted PIN block.
        /// [Required] writeOnly
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// The format of the encrypted PIN block.
        /// [Required] writeOnly
        /// </summary>
        public string BlockFormat { get; set; }
    }
}
