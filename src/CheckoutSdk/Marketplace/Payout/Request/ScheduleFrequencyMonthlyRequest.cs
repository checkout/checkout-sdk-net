namespace Checkout.Marketplace.Payout.Request
{
    public class ScheduleFrequencyMonthlyRequest : ScheduleRequest
    {
        public int? ByMonthDay { get; set; }

        public ScheduleFrequencyMonthlyRequest() : base(ScheduleFrequency.Monthly)
        {
        }
    }
}