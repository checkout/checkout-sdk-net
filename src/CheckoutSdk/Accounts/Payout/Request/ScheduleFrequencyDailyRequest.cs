namespace Checkout.Accounts.Payout.Request
{
    /// <summary>
    /// A daily payout schedule.
    /// For SaaS seller (ISV) sub-entities this runs on working days only, Monday to Friday, with
    /// no payout at weekends, and is based on the available balance as of 00:00 in the
    /// sub-entity's time zone. Standard sub-entities are paid out every day.
    /// </summary>
    public class ScheduleFrequencyDailyRequest : ScheduleRequest
    {
        public ScheduleFrequencyDailyRequest() : base(ScheduleFrequency.Daily)
        {
        }
    }
}