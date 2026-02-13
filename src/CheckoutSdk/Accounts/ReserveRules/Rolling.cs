namespace Checkout.Accounts.ReserveRules
{
    public class Rolling
    {
        public decimal? Percentage { get; set; }

        public HoldingDuration HoldingDuration { get; set; }
    }
}