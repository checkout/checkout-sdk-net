using System.Collections.Generic;

namespace Checkout.Accounts.Payout.Request
{
    /// <summary>
    /// A weekly payout schedule.
    /// SaaS seller (ISV) sub-entities accept working days only, Monday to Friday: a schedule set
    /// to a Saturday or Sunday is rejected. Their payout is based on the available balance as of
    /// 00:00 in the sub-entity's time zone. Standard sub-entities accept any day.
    /// </summary>
    public class ScheduleFrequencyWeeklyRequest : ScheduleRequest
    {
        public IList<DaySchedule> ByDay { get; set; }

        public ScheduleFrequencyWeeklyRequest() : base(ScheduleFrequency.Weekly)
        {
        }
    }
}