using System.Collections.Generic;

namespace Checkout.Marketplace.Payout.Response
{
    public class ScheduleFrequencyWeeklyResponse : ScheduleResponse
    {
        public IList<DaySchedule> ByDay { get; set; }

        public ScheduleFrequencyWeeklyResponse() : base(ScheduleFrequency.Weekly)
        {
        }
    }
}