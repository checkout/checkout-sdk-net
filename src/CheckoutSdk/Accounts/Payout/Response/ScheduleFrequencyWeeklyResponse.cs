namespace Checkout.Accounts.Payout.Response
{
    public class ScheduleFrequencyWeeklyResponse : ScheduleResponse
    {
        public DaySchedule? ByDay { get; set; }

        public ScheduleFrequencyWeeklyResponse() : base(ScheduleFrequency.Weekly)
        {
        }
    }
}