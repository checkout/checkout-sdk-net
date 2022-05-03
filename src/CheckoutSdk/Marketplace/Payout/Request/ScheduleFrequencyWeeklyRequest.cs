namespace Checkout.Marketplace.Payout.Request
{
    public class ScheduleFrequencyWeeklyRequest : ScheduleRequest
    {
        public DaySchedule? ByDay { get; set; }

        public ScheduleFrequencyWeeklyRequest() : base(ScheduleFrequency.Weekly)
        {
        }
    }
}