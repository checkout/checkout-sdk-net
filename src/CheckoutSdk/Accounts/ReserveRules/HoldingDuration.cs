namespace Checkout.Accounts.ReserveRules
{
    public class HoldingDuration
    {
        /// <summary>
        /// Number of weeks the collateral balance will be reserved for
        /// [Required]
        /// </summary>
        public int? Weeks { get; set; }
    }
}