namespace Checkout.Balances
{
    /// <summary>
    /// A breakdown of the funds held in the collateral balance.
    /// </summary>
    public class CollateralBreakdown
    {
        /// <summary>
        /// The portion of the collateral balance held as a fixed reserve.
        /// [Required]
        /// </summary>
        public long? FixedReserve { get; set; }

        /// <summary>
        /// The portion of the collateral balance held as a rolling reserve.
        /// [Required]
        /// </summary>
        public long? RollingReserve { get; set; }
    }
}
