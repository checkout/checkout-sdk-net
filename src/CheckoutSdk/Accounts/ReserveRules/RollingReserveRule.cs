namespace Checkout.Accounts.ReserveRules
{
    public class RollingReserveRule
    {
        /// <summary>
        /// The percentage of captured funds that will be reserved as a collateral balance
        /// [Required]
        /// </summary>
        public decimal? Percentage { get; set; }

        /// <summary>
        /// The length of time the collateral balance will be reserved for
        /// [Required]
        /// </summary>
        public HoldingDuration HoldingDuration { get; set; }
    }
}