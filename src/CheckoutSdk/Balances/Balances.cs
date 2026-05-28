namespace Checkout.Balances
{
    public class Balances
    {
        public long? Pending { get; set; }

        public long? Available { get; set; }

        public long? Payable { get; set; }

        public long? Collateral { get; set; }

        /// <summary>
        /// A breakdown of the funds held in the collateral balance.
        /// [Optional]
        /// </summary>
        public CollateralBreakdown CollateralBreakdown { get; set; }
    }

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