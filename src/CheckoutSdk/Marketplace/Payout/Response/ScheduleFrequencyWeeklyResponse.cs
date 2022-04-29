namespace Checkout.Marketplace.Payout.Response
{
    public class ScheduleFrequencyWeeklyResponse : ScheduleResponse
    {
        public DaySchedule? ByDay { get; set; }

        public ScheduleFrequencyWeeklyResponse() : base(ScheduleFrequency.Weekly)
        {
        }
    }
}