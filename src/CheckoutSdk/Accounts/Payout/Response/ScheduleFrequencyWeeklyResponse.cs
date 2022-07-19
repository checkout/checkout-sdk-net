using System.Collections.Generic;

namespace Checkout.Accounts.Payout.Response
{
    public class ScheduleFrequencyWeeklyResponse : ScheduleResponse
    {
        public IList<DaySchedule> ByDay { get; set; }

        public ScheduleFrequencyWeeklyResponse() : base(ScheduleFrequency.Weekly)
        {
        }
    }
}