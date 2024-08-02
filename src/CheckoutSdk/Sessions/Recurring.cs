namespace Checkout.Sessions
{
    public class Recurring
    {
        public long? DaysBetweenPayments { get; set; } = 1L;

        public string Expiry { get; set; } = "99991231";
    }
}