namespace Checkout.Accounts.Payout.Request
{
    public abstract class ScheduleRequest
    {
        public ScheduleFrequency? Frequency { get; set; }

        protected ScheduleRequest(ScheduleFrequency frequency)
        {
            Frequency = frequency;
        }
    }
}