using System.Collections.Generic;

namespace Checkout.Accounts.Payout.Request
{
    public class ScheduleFrequencyWeeklyRequest : ScheduleRequest
    {
        public IList<DaySchedule> ByDay { get; set; }

        public ScheduleFrequencyWeeklyRequest() : base(ScheduleFrequency.Weekly)
        {
        }
    }
}