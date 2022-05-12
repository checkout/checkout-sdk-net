namespace Checkout.Marketplace.Payout.Response
{
    public class ScheduleResponse
    {
        public ScheduleFrequency? Type { get; set; }

        public ScheduleResponse(ScheduleFrequency? type)
        {
            Type = type;
        }
    }
}