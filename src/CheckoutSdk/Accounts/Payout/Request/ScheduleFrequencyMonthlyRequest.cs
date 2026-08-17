using System.Collections.Generic;

namespace Checkout.Accounts.Payout.Request
{
    /// <summary>
    /// A monthly payout schedule.
    /// SaaS seller (ISV) sub-entities accept only these combinations, in any order: [1], [15],
    /// [1, 15] or [1, 16]. Their payout is based on the available balance as of 00:00 in the
    /// sub-entity's time zone. Standard sub-entities accept any day from 1 to 28.
    /// </summary>
    public class ScheduleFrequencyMonthlyRequest : ScheduleRequest
    {
        public IList<int> ByMonthDay { get; set; }

        public ScheduleFrequencyMonthlyRequest() : base(ScheduleFrequency.Monthly)
        {
        }
    }
}