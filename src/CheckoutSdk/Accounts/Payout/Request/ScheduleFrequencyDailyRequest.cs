namespace Checkout.Accounts.Payout.Request
{
    public class ScheduleFrequencyDailyRequest : ScheduleRequest
    {
        public ScheduleFrequencyDailyRequest() : base(ScheduleFrequency.Daily)
        {
        }
    }
}