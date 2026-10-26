using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The encrypted PIN block details. Used only by CardPresent, which the Payment Setups API specification
    /// removed.
    /// </summary>
    [Obsolete("card_present is no longer part of the Payment Setups API specification. There is no replacement. Will be removed in the next major version.")]
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
