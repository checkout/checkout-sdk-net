namespace Checkout.Accounts.Payout.Response
{
    public class CurrencySchedule
    {
        public bool? Enabled { get; set; }

        public int? Threshold { get; set; }

        public ScheduleResponse Recurrence { get; set; }
    }
}