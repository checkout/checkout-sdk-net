namespace Checkout.Marketplace.Payout.Response
{
    public class CurrencySchedule
    {
        public bool? Enabled { get; set; }

        public int? Threshold { get; set; }

        public ScheduleResponse Recurrence { get; set; }
    }
}