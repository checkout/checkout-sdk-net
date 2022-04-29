namespace Checkout.Marketplace.Payout.Response
{
    public class ScheduleFrequencyMonthlyResponse : ScheduleResponse
    {
        public int? ByMonthDay { get; set; }

        public ScheduleFrequencyMonthlyResponse() : base(ScheduleFrequency.Monthly)
        {
        }
    }
}