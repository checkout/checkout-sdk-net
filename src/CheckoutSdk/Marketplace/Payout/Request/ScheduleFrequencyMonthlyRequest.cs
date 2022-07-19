using System.Collections.Generic;

namespace Checkout.Marketplace.Payout.Request
{
    public class ScheduleFrequencyMonthlyRequest : ScheduleRequest
    {
        public IList<int> ByMonthDay { get; set; }

        public ScheduleFrequencyMonthlyRequest() : base(ScheduleFrequency.Monthly)
        {
        }
    }
}