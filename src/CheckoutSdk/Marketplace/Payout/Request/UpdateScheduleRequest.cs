namespace Checkout.Marketplace.Payout.Request
{
    public class UpdateScheduleRequest
    {
        public bool? Enabled { get; set; }

        public int? Threshold { get; set; }

        public ScheduleRequest Recurrence { get; set; }
    }
}