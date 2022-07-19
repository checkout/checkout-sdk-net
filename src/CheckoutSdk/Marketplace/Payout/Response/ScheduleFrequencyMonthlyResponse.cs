using System.Collections.Generic;

namespace Checkout.Marketplace.Payout.Response
{
    public class ScheduleFrequencyMonthlyResponse : ScheduleResponse
    {
        public IList<int> ByMonthDay { get; set; }

        public ScheduleFrequencyMonthlyResponse() : base(ScheduleFrequency.Monthly)
        {
        }
    }
}