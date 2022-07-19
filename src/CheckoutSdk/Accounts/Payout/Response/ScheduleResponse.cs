namespace Checkout.Accounts.Payout.Response
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